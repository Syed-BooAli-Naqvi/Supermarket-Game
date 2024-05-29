using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableDisableEvents : MonoBehaviour
{
    public UnityEvent Onenable, Ondisable;

    private void OnEnable()
    {
        Onenable?.Invoke();
    }
    public void ENABLE()
    {
        Onenable?.Invoke();
    }
    private void OnDisable()
    {
        Ondisable?.Invoke();
    }
    public void DISABLE()
    {
        Ondisable?.Invoke();
    }
}