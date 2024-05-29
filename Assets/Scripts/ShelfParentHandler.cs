using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfParentHandler : MonoBehaviour
{
    public ShelfType shelfType;
    public Transform point;
    public List<ShelfHandler> shelfHandlers;
    public bool isBought, canCheck;
    public int starter;
    public bool doNotTouchThis;
    private void OnValidate()
    {
        if (doNotTouchThis)
        {
            doNotTouchThis = false;
            for (int i = 0; i < transform.childCount - 2; i++)
            {
                transform.GetChild(i).name = "Shelf (" + (starter + i) + ")";
            }
        }
    }
    private void Start()
    {
        CheckBought();
    }
    public void CheckBought()
    {
        if (canCheck)
            if (PlayerPrefs.HasKey("CheckShelfIsBought" + name))
                isBought = PlayerPrefsExtra.GetBool("CheckShelfIsBought" + name);
        gameObject.SetActive(isBought);
    }
    public void Buy()
    {
        PlayerPrefsExtra.SetBool("CheckShelfIsBought" + name, true);
        CheckBought();
    }
}
