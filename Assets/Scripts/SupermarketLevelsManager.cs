using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SupermarketLevelsManager : MonoBehaviour
{

    public static SupermarketLevelsManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    private void OnDisable()
    {
        Instance = null;
    }

    public Button interactHandBtn;
    public static Button InteractHandBtn { get { return Instance.interactHandBtn; } }
    public static GameObject InteractHandObj { get { return Instance.interactHandBtn.gameObject; } }

    public CanvasGroup cFCG;
    public static CanvasGroup CFCG { get { return Instance.cFCG; } }

    public GameObject[] rccThings, playerThings;
    public TMP_Text cashTxt;

    private void Update()
    {
        cashTxt.text = PlayerPrefs.GetInt("Market1Cash").ToString();
    }

    public void AddOrRemoveCash(int cash)
    {
        PlayerPrefs.SetInt("Market1Cash", PlayerPrefs.GetInt("Market1Cash") + cash);
    }

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

    public void DisableInteractBtn()
    {
        InteractHandObj.SetActive(false);
        InteractHandBtn.onClick.RemoveAllListeners();
    }


    public GameObject PausePanel;
    public GameObject LevelCompletePanel;
    public GameObject LevelFailedPanel;

    public void ShowLevelCompletePanel()
    {
        PlayerLevelManager.IncreasePlayerLevel();
        LevelCompletePanel.SetActive(true);
    }

    public void ShowlevelFail()
    {
        LevelFailedPanel.SetActive(true);
    }
    public void ShowLevelComplteWithDelay(float time)
    {
        StartCoroutine(ShowCompleteWithDelay(time));
    }

    IEnumerator ShowCompleteWithDelay(float time)
    {
        yield return new WaitForSeconds(time);
        ShowLevelCompletePanel();
    }

    public void ShowLevelFailWithDelay(float time)
    {
        StartCoroutine(ShowFailWithDelay(time));
    }

    IEnumerator ShowFailWithDelay(float time)
    {
        yield return new WaitForSeconds(time);
        ShowlevelFail();
    }

    public void gotohome()
    {
        StartCoroutine(LoadingScript.Instance.AsynchronousLoad(1));
    }
    public void gotohome1()
    {
        PlayerPrefs.SetInt("SuperLevel",
    PlayerPrefs.GetInt("SuperLevel") + 1);
        StartCoroutine(LoadingScript.Instance.AsynchronousLoad(1));
    }

    public void reloadGame()
    {
        StartCoroutine(LoadingScript.Instance.AsynchronousLoad(2));
    }

    public void reloadGame1()
    {
        PlayerPrefs.SetInt("SuperLevel",
    PlayerPrefs.GetInt("SuperLevel") + 1);
        StartCoroutine(LoadingScript.Instance.AsynchronousLoad(2));
    }

    public void Nextbtnlevel()
    {
        int currentLevel = PlayerLevelManager.GetPlayerLevel();

        if (currentLevel < FindObjectOfType<LevelManager>().levels.Count)
        {
            PlayerLevelManager.SetPlayerLevel(currentLevel + 1);
            PlayerPrefs.SetInt("SuperLevel",
        PlayerPrefs.GetInt("SuperLevel") + 1);
            StartCoroutine(LoadingScript.Instance.AsynchronousLoad(2));
        }
        else
        {
            StartCoroutine(LoadingScript.Instance.AsynchronousLoad(1));
        }
    }

    IEnumerator reloadingaftersometime()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(LoadingScript.Instance.AsynchronousLoad(2));
    }
    public void Gamepause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0.01f;
    }

    public void GameResume()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

}
