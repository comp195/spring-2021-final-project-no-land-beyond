using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool interact_tag;
    [Header("Tag")]
    [SerializeField] string tag;
    private void OnTriggerEnter(Collider other)
    {
        if (interact_tag && other.transform.CompareTag(tag))
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}
