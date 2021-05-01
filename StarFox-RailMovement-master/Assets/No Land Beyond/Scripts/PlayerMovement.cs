using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement : MonoBehaviour
{
    private Transform playerModel;
    public EffectsScript effectsManager;

    [Header("Settings")]
    public bool joystick = true;

    [Space]

    [Header("Parameters")]
    public float xySpeed = 18;
    public float lookSpeed = 340;
    public float forwardSpeed = 6;

    [Space]

    [Header("Public References")]
    public Transform aimTarget;
    public CinemachineDollyCart dolly;
    public Transform cameraParent;

    public GameObject leftGun;
    public GameObject rightGun;
    public GameObject middleGun;

    private int upgrades;

    private bool paused;
    public bool collidingWithPowerUp = false;

    public SoundFX sounds;

    public HUD hud;

    void Awake(){
        if(hud == null){
            hud = new HUD();
        }
    }
    void Start()
    {
        upgrades = 0;
        paused = false;
        playerModel = transform.GetChild(0);
        SetSpeed(forwardSpeed);
    }

    void Update()
    {
        float h = joystick ? Input.GetAxis("Horizontal") : Input.GetAxis("Mouse X");
        float v = joystick ? Input.GetAxis("Vertical") : Input.GetAxis("Mouse Y");

        LocalMove(h, v, xySpeed);
        RotationLook(h,v, lookSpeed);
        HorizontalLean(playerModel, h, 80, .1f);

        if (Input.GetButtonDown("Action"))
            Boost(true);

        if (Input.GetButtonUp("Action"))
            Boost(false);

        if (Input.GetButtonDown("Fire3"))
            Break(true);

        if (Input.GetButtonUp("Fire3"))
            Break(false);

        if (Input.GetButtonDown("Fire2"))
            Pause();

        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage();
        }


        if (Input.GetButtonDown("TriggerL") || Input.GetButtonDown("TriggerR"))
        {
            int dir = Input.GetButtonDown("TriggerL") ? -1 : 1;
            QuickSpin(dir);
        }


    }

    void Pause()
    {
    	if(!paused){
    		Time.timeScale = 0;
    		paused = true;
    	}
    	else{
    		Time.timeScale = 1;
    		paused = false;
    	}
    }

    void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);

    }

    public void QuickSpin(int dir)
    {
        if (!DOTween.IsTweening(playerModel))
        {
            playerModel.DOLocalRotate(new Vector3(playerModel.localEulerAngles.x, playerModel.localEulerAngles.y, 360 * -dir), .4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
            effectsManager.QuickSpin(dir);
        }
    }

    void SetSpeed(float x)
    {
        dolly.m_Speed = x;
    }

    void SetCameraZoom(float zoom, float duration)
    {
        cameraParent.DOLocalMove(new Vector3(0, 0, zoom), duration);
    }

    void DistortionAmount(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<LensDistortion>().intensity.value = x;
    }

    void FieldOfView(float fov)
    {
        cameraParent.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov;
    }

    void Chromatic(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>().intensity.value = x;
    }


    void Boost(bool state)
    {

        if (state)
        {
            cameraParent.GetComponentInChildren<CinemachineImpulseSource>().GenerateImpulse();
        }

        effectsManager.Boost(state);

        float origFov = state ? 40 : 55;
        float endFov = state ? 55 : 40;
        float origChrom = state ? 0 : 1;
        float endChrom = state ? 1 : 0;
        float origDistortion = state ? 0 : -30;
        float endDistorton = state ? -30 : 0;
        float speed = state ? forwardSpeed * 2 : forwardSpeed;
        float zoom = state ? -7 : 0;

        DOVirtual.Float(origChrom, endChrom, .5f, Chromatic);
        DOVirtual.Float(origFov, endFov, .5f, FieldOfView);
        DOVirtual.Float(origDistortion, endDistorton, .5f, DistortionAmount);

        DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }

    void Break(bool state)
    {
        float speed = state ? forwardSpeed / 3 : forwardSpeed;
        float zoom = state ? 3 : 0;

        DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }

    void TakeDamage(){
        hud.TakeDamage();
    }

    void Die(){
    	//Death event
    	//TODO: switch to death screen (if exists) or start menu
    	
    }

    public void Explode() {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(playerModel);

        Destroy(gameObject, 5f);
    }

    public void IncrementKills(){
        hud.IncrementKills();
    }

    public void ComputeScore(){
        hud.ComputeScore();
    }

    public void UpgradeWeapon(bool firstUpgrade){
        if(firstUpgrade){
        	// two bullets instead of 1
        	middleGun.SetActive(false);
        	leftGun.SetActive(true);
        	rightGun.SetActive(true);
        }
        else{
        	// change bullets
        	Shoot shoot1 = leftGun.gameObject.GetComponent<Shoot>();
        	Shoot shoot2 = rightGun.gameObject.GetComponent<Shoot>();
        	shoot1.UpgradeWeapon();
        	shoot2.UpgradeWeapon();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    	RingScript powerUp = other.gameObject.GetComponent<RingScript>();
    	if(powerUp == null){
    		collidingWithPowerUp = false;
    	}
        Projectile bullet = other.gameObject.GetComponent<Projectile>(); 
        if(bullet != null && !bullet.fromPlayer){
            sounds.Ouch(true);
            TakeDamage();
        }
        else{
        if(powerUp != null && !collidingWithPowerUp){
        	sounds.PowerUp(true);
        	bool firstUpgrade = upgrades == 0;
            UpgradeWeapon(firstUpgrade);
            upgrades++;
        }

        }
    }
}
