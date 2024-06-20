using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupermarketLevelsManager : Singleton<SupermarketLevelsManager>
{
    public GameObject[] rccThings, playerThings;

    public void ChangeToRcc()
    {
        for (int i = 0; i < rccThings.Length; i++)
        {
            rccThings[i].SetActive(true);
        }
        for (int i = 0; i < playerThings.Length; i++)
        {
            playerThings[i].SetActive(false);
        }
    }

    public void ChangeToPlayer()
    {
        for (int i = 0; i < rccThings.Length; i++)
        {
            rccThings[i].SetActive(false);
        }
        for (int i = 0; i < playerThings.Length; i++)
        {
            playerThings[i].SetActive(true);
        }
    }
}
