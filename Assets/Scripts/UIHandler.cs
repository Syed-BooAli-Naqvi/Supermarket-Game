using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : Singleton<UIHandler>
{
    public Button interactHandBtn;
    public static Button InteractHandBtn { get { return Instance.interactHandBtn; } }
    public static GameObject InteractHandObj { get { return Instance.interactHandBtn.gameObject; } }

    public CanvasGroup cFCG;
    public static CanvasGroup CFCG { get { return Instance.cFCG; } }

    public Button dropBoxBtn;
    public static Button DropBoxBtn { get { return Instance.dropBoxBtn; } }

    public Button disposeBoxBtn;
    public static Button DisposeBoxBtn { get { return Instance.disposeBoxBtn; } }

    public ShelfItemPriceUIHandler shelfItemPriceUIHandler;
    public static ShelfItemPriceUIHandler ShelfItemPriceUI { get { return Instance.shelfItemPriceUIHandler; } }

    public Text[] cashTxt, lightTxt;

    public Slider music, sound;

    private void Start()
    {
        music.value = PlayerPrefs.GetFloat(SharedPrefs.MusicLevel, 1);
        sound.value = PlayerPrefs.GetFloat(SharedPrefs.SoundLevel, 1);
        music.onValueChanged.AddListener(delegate
        {
            SetMusic(music);
        });
        sound.onValueChanged.AddListener(delegate
        {
            SetSound(sound);
        });
    }

    public void RewardPlayer()
    {
        PlayerPrefs.SetFloat(SharedPref.Cash, PlayerPrefs.GetFloat(SharedPref.Cash, 100) + 100);
    }
    public void RewardLightPlayer()
    {
        PlayerPrefs.SetInt(SharedPref.Light, PlayerPrefs.GetInt(SharedPref.Light, 50) + 100);
    }

    private void Update()
    {
        foreach (var item in cashTxt)
        {
            item.text = PlayerPrefs.GetFloat(SharedPref.Cash, 100).ToString("#.00");
        }
        foreach (var item in lightTxt)
        {
            item.text = PlayerPrefs.GetInt(SharedPref.Light, 50).ToString();
        }
    }

    public static void DisableInteractBtn()
    {
        InteractHandObj.SetActive(false);
        InteractHandBtn.onClick.RemoveAllListeners();
    }


    public void SetMusic(Slider slider)
    {
        SoundManager.Instance.SetMusicLevel(slider.value);
    }

    public void SetSound(Slider slider)
    {
        SoundManager.Instance.SetSoundLevel(slider.value);
    }

    public void GoToHome()
    {
        StartCoroutine(LoadingScript.Instance.AsynchronousLoad(1));
    }


    public void Restart()
    {
        StartCoroutine(LoadingScript.Instance.AsynchronousLoad(3));
    }
}
