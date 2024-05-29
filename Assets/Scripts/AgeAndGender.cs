using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AgeAndGender : MonoBehaviour
{
    public Slider ageSlider;
    public TMP_Text age;
    public TMP_Dropdown gender;

    private void OnEnable()
    {
        gameObject.SetActive(PlayerPrefs.GetInt("AgeOK") == 1);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt(SharedPref.UserAge) == 1)
        {
            ageSlider.value = PlayerPrefs.GetFloat(SharedPref.CurrentUserAge);
        }
        else
        {
            ageSlider.value = 17;
        }
        gender.SetValueWithoutNotify(PlayerPrefs.GetInt(SharedPref.CurrentUserGender));
        SetAge();
        ageSlider.onValueChanged.AddListener(delegate
        {
            SetAge();
	    });
        gender.onValueChanged.AddListener(delegate
        {
            SaveGender();
        });
    }
    private void SetAge()
    {
        age.text = ageSlider.value + " Years";
    }
    public void SaveAge()
    {
        PlayerPrefs.GetInt("AgeOK", 1);
        PlayerPrefs.SetInt(SharedPref.UserAge, 1);
        PlayerPrefs.SetFloat(SharedPref.CurrentUserAge, ageSlider.value);
        gameObject.SetActive(false);
    }
    public void SaveGender()
    {
        PlayerPrefs.SetInt(SharedPref.CurrentUserGender, gender.value);
    }
}
