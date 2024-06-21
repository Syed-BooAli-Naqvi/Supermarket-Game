using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterExit : MonoBehaviour
{
    public UnityEvent OnTriggerEnterEvent, OnTriggerExitEvent;
    public string EnterTag, ExitTag;
    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(EnterTag))
            if (other.CompareTag(EnterTag))
            {
                OnTriggerEnterEvent.Invoke();
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(ExitTag))
            if (other.CompareTag(ExitTag))
            {
                OnTriggerExitEvent.Invoke();
            }
    }
}