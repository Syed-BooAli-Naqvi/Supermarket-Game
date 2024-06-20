using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<GameObject> levels; // List of GameObjects representing levels

    private int currentLevelIndex = 0; // Index of the current level

    void Start()
    {
        // Load the player's current level from PlayerPrefs
        currentLevelIndex = PlayerLevelManager.GetPlayerLevel() - 1; // Player level starts from 1, list index starts from 0

        // Ensure that currentLevelIndex is within the bounds of the levels list
        if (currentLevelIndex < 0 || currentLevelIndex >= levels.Count)
        {
            Debug.LogError("Invalid current level index!");
            currentLevelIndex = 0; // Reset to the first level
        }

        // Activate the current level
        ActivateCurrentLevel();
    }

    // Function to activate the current level and deactivate other levels
    private void ActivateCurrentLevel()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(i == PlayerPrefs.GetInt("SuperLevel"));
        }
    }

    // Function to advance to the next level
    public void NextLevel()
    {
        if (currentLevelIndex < levels.Count - 1)
        {
            currentLevelIndex++;
            PlayerLevelManager.IncreasePlayerLevel(); // Increase player's level
            ActivateCurrentLevel();
        }
    }

    // Function to go back to the previous level
    public void PreviousLevel()
    {
        if (currentLevelIndex > 0)
        {
            currentLevelIndex--;
            PlayerLevelManager.DecreasePlayerLevel(); // Decrease player's level
            ActivateCurrentLevel();
        }
    }
}

public class PlayerLevelManager
{
    // Define the key for storing the player's level in PlayerPrefs
    private const string PlayerLevelKey = "PlayerLevel";

    // Default starting level for the player
    private const int DefaultStartingLevel = 1;

    // Function to retrieve the player's current level
    public static int GetPlayerLevel()
    {
        // If the player level exists in PlayerPrefs, return it
        if (PlayerPrefs.HasKey(PlayerLevelKey))
        {
            return PlayerPrefs.GetInt(PlayerLevelKey);
        }
        // If not, set the default starting level and return it
        else
        {
            SetPlayerLevel(DefaultStartingLevel);
            return DefaultStartingLevel;
        }
    }

    // Function to set the player's current level
    public static void SetPlayerLevel(int level)
    {
        PlayerPrefs.SetInt(PlayerLevelKey, level);
        PlayerPrefs.Save(); // Save changes to PlayerPrefs
    }

    // Function to increase the player's level
    public static void IncreasePlayerLevel()
    {
        if (PlayerPrefs.GetInt("SuperLevel") == GetPlayerLevel())
        {
            int currentLevel = GetPlayerLevel();
            SetPlayerLevel(currentLevel + 1);
        }
    }

    // Function to decrease the player's level
    public static void DecreasePlayerLevel()
    {
        int currentLevel = GetPlayerLevel();
        if (currentLevel > 1) // Make sure level doesn't go below 1
        {
            SetPlayerLevel(currentLevel - 1);
        }
    }

    // Function to reset the player's level to the default starting level
    public static void ResetPlayerLevel()
    {
        SetPlayerLevel(DefaultStartingLevel);
    }
}