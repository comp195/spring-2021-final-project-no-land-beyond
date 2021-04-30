using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RingScript : MonoBehaviour
{
    public float speed;
    public bool activated;

    // Update is called once per frame
    void Update()
    {
        if(!activated)
            transform.eulerAngles += new Vector3(0, speed, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Collect(other));
        }
    }

    IEnumerator Collect(Collider other){
            activated = true;
            transform.parent = other.transform.parent;
            transform.parent.GetComponent<PlayerMovement>().collidingWithPowerUp = true;

            Sequence s = DOTween.Sequence();

            s.Append(transform.DORotate(Vector3.zero, .2f));
            s.Append(transform.DORotate(new Vector3(0, 0, -900), 3, RotateMode.LocalAxisAdd));
            s.Join(transform.DOScale(0, .5f).SetDelay(1f));
        yield return s.AppendCallback(() => Destroy(gameObject));      
    }
}
