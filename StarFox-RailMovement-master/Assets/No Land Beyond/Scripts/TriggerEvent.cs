using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TriggerEvent : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool destroy_after;
    [SerializeField] bool has_delay;
    [SerializeField] bool interact_with_tag;

    [Header("Events")]
    [SerializeField] UnityEvent events;

    [Header("Delay")]
    [SerializeField] float delay_time;

    [Header("Tag")]
    [SerializeField] string interact_tag;

    void Execute()
    {
        if (has_delay)
        {
            StartCoroutine(DelayEvent());
            return;
        }
        else
        {
            events.Invoke();
        }
        if (destroy_after)
            Destroy(gameObject);
    }

    IEnumerator DelayEvent()
    {
        yield return new WaitForSeconds(delay_time);
        events.Invoke();
        if (destroy_after)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interact_with_tag && other.transform.CompareTag(interact_tag))
            Execute();
        else if (!interact_with_tag)
            Execute();
    }

    private void OnTriggerExit(Collider other)
    {
        if (interact_with_tag && other.transform.CompareTag(interact_tag))
            Execute();
        else if (!interact_with_tag)
            Execute();
    }
}
