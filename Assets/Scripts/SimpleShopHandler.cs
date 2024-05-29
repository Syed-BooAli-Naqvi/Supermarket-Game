using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShopHandler : MonoBehaviour
{
    public void GiveCash(float howMuch)
    {
        PlayerPrefs.SetFloat(SharedPref.Cash, PlayerPrefs.GetFloat(SharedPref.Cash, 100) + howMuch);
    }
    public void GiveGems(int howMuch)
    {
        PlayerPrefs.SetInt(SharedPref.Light, PlayerPrefs.GetInt(SharedPref.Light, 50) + howMuch);
    }
}
