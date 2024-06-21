using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{
    public Text[] coinTxt, lightTxt;
    public GameObject settingPopup;
    public Slider music, sound;
    public GameObject[] deals;
    private void Start()
    {
        //deals.SetActive(true);
        music.value = PlayerPrefs.GetFloat(SharedPrefs.MusicLevel, 1);
        sound.value = PlayerPrefs.GetFloat(SharedPrefs.SoundLevel, 1);

        //if (AdsHandler.bigBannerAvailable)
        //{
        //    AdsHandler.instance.HideBigBannerAds();
        //    AdsHandler.instance.ShowBannerAds();
        //}
        HabdleDeals();
    }

    public void HabdleDeals()
    {
        //deals[Random.Range(0, deals.Length)].SetActive(true);
    }

    private void Update()
    {
        foreach (var item in coinTxt)
            item.text = PlayerPrefs.GetFloat(SharedPref.Cash,100).ToString("#.00");
        foreach (var item in lightTxt)
            item.text = PlayerPrefs.GetInt(SharedPref.Light,50).ToString();
    }

    public void OpenUrl(string url)
    {
        SoundManager.Instance.PlaySound(SoundName.ButtonClick);
        Application.OpenURL(url);
    }

    public void OpenSetting()
    {
        //if (AdsHandler.bigBannerAvailable)
        //{
        //    AdsHandler.instance.HideBannerAds();
        //    AdsHandler.instance.ShowBigBannerAds();
        //}
        SoundManager.Instance.PlaySound(SoundName.ButtonClick);
        settingPopup.SetActive(true);
    }

    public void CloseSetting()
    {
        //if (AdsHandler.bigBannerAvailable)
        //{
        //    AdsHandler.instance.HideBigBannerAds();
        //    AdsHandler.instance.ShowBannerAds();
        //}
        SoundManager.Instance.PlaySound(SoundName.ButtonClick);
        settingPopup.SetActive(false);
    }

    public void StartGame(int sceneNum)
    {
        SoundManager.Instance.PlaySound(SoundName.ButtonClick);
        //AdsHandler.instance.callInterstitialwithCounter();
        StartCoroutine(LoadingScript.Instance.AsynchronousLoad(sceneNum));
    }

    public void RewardPlayer()
    {
        SoundManager.Instance.PlaySound(SoundName.ButtonClick);
        PlayerPrefs.SetFloat(SharedPref.Cash, PlayerPrefs.GetFloat(SharedPref.Cash, 100) + 100);
    }

    public void SetMusic(Slider slider)
    {
        SoundManager.Instance.SetMusicLevel(slider.value);
    }

    public void SetSound(Slider slider)
    {
        SoundManager.Instance.SetSoundLevel(slider.value);
    }

    public void Restore()
    {
        //Restore Purchase
        //InAppManager.Instance.RestorePurchases();
    }
}
