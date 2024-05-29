using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeNameHandler : MonoBehaviour
{
    public TMP_Text[] shopNames;
    public TMP_InputField tMP_InputField;
    public GameObject panel;

    public void Start()
    {
        tMP_InputField.onValueChanged.RemoveAllListeners();
        tMP_InputField.onEndEdit.RemoveAllListeners();
        tMP_InputField.onValueChanged.AddListener(delegate
        {
            Save();
        });
        tMP_InputField.onEndEdit.AddListener(delegate
        {
            Save();
        });
        for (int i = 0; i < shopNames.Length; i++)
        {
            shopNames[i].text = PlayerPrefs.GetString(SharedPrefs.ShopName, "SUPERMARKET").ToUpper();
        }
    }

    public void Save()
    {
        if (tMP_InputField.text.Length < 21)
        {
            PlayerPrefs.SetString(SharedPrefs.ShopName, tMP_InputField.text.ToUpper());
            for (int i = 0; i < shopNames.Length; i++)
            {
                shopNames[i].text = PlayerPrefs.GetString(SharedPrefs.ShopName, "SUPERMARKET").ToUpper();
            }
            tMP_InputField.text = PlayerPrefs.GetString(SharedPrefs.ShopName, "SUPERMARKET").ToUpper();
        }
        tMP_InputField.text = tMP_InputField.text.ToUpper();
    }
    public void ButtonSave()
    {
        Save();
        PlayerPrefs.SetInt(SharedPref.NameChangeTut, 1);
        if (PlayerPrefs.GetInt(SharedPref.ShopOpenCloseTut)!=1)
        {
            TutorialManager.Instance.StarDoorOpenTut();
        }
    }
    public void StartNameChange()
    {
        Start();
        panel.SetActive(true);
    }
}
