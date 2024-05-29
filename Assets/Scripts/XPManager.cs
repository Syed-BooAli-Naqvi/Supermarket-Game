using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPManager : Singleton<XPManager>
{
    public Image progressBar; // Reference to your progress bar image
    public Text progressText,xpTxt; // Reference to the UI Text for displaying progress
    public List<int> XPThresholds; // List of XP thresholds for each level
    public List<int> StoreLevels; // List of store levels based on XP thresholds

    public int currentXP = 0; // Current XP value
    public int currentLevel = 0; // Current level

    public static string XPKey = "PlayerXP";
    public static string LevelKey = "PlayerLevel";

    public bool test;
    private void OnValidate()
    {
        if (test)
        {
            test = false;
            GainXP(5);
        }
    }

    private void Start()
    {
        LoadPlayerProgress();
        UpdateProgressBar();
    }

    // Call this method whenever XP is gained
    public void GainXP(int xp)
    {
        currentXP += xp;
        UpdateLevel();
        UpdateProgressBar();
        SavePlayerProgress();
    }

    // Update current level based on XP
    private void UpdateLevel()
    {
        for (int i = 0; i < XPThresholds.Count; i++)
        {
            if (currentXP >= XPThresholds[i])
            {
                currentLevel = i;
                if (currentLevel > PlayerPrefs.GetInt(LevelKey))
                {
                    Debug.Log("When the hell does code comes here");
                    PlayerPrefs.SetInt(SharedPref.CanBuySimpleShelf, PlayerPrefs.GetInt(SharedPref.CanBuySimpleShelf) + 1);
                    PlayerPrefs.SetInt(SharedPref.CanBuyFruitShelf, PlayerPrefs.GetInt(SharedPref.CanBuyFruitShelf) + 1);
                }
            }
            else
            {
                break;
            }
        }
    }

    // Update progress bar fill amount and text
    private void UpdateProgressBar()
    {
        if (currentLevel < StoreLevels.Count)
        {
            float progress = (float)(currentXP - XPThresholds[currentLevel]) / (float)(XPThresholds[currentLevel + 1] - XPThresholds[currentLevel]);
            progressBar.fillAmount = progress;

            if (progressText != null)
            {
                progressText.text = $"Level {currentLevel + 1}";
            }
            if (xpTxt != null)
            {
                xpTxt.text = $"{currentXP} / {XPThresholds[currentLevel + 1]} XP";
            }
        }
        else
        {
            // Store level is maxed out
            progressBar.fillAmount = 1f; // Fill the progress bar completely
            if (progressText != null)
            {
                progressText.text = $"Level Maxed";
            }
            if (xpTxt != null)
            {
                xpTxt.text = $"{currentXP} XP";
            }
        }
    }


    // Save player's XP and level
    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt(XPKey, currentXP);
        PlayerPrefs.SetInt(LevelKey, currentLevel);
        PlayerPrefs.Save();
    }

    // Load player's XP and level
    private void LoadPlayerProgress()
    {
        if (PlayerPrefs.HasKey(XPKey))
        {
            currentXP = PlayerPrefs.GetInt(XPKey);
        }

        if (PlayerPrefs.HasKey(LevelKey))
        {
            currentLevel = PlayerPrefs.GetInt(LevelKey);
        }
    }
}
