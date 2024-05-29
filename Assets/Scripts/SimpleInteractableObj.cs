using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UIHandler;

public class SimpleInteractableObj : MonoBehaviour,IInteractable
{
    public UnityEvent instantInteractEvent, interactEvent;
    public UnityEvent instantNonInteractEvent;
    public bool canSimpleInteract = true;
    public bool canChange = true;

    public void Interact()
    {
        instantInteractEvent.Invoke();
        if (canSimpleInteract)
        {
            InteractHandBtn.gameObject.SetActive(true);
            InteractHandBtn.onClick.AddListener(() =>
            {
                if (canChange)
                    CFCG.alpha = 0;
                interactEvent.Invoke();
                InteractHandBtn.gameObject.SetActive(false);
                InteractHandBtn.onClick.RemoveAllListeners();
            });
        }
    }

    public void NonInteract()
    {
        instantNonInteractEvent.Invoke();
    }
}
