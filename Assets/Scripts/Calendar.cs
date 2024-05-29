using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Calendar : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text dayText;

    private bool colonVisible = true; // Flag to toggle colon visibility
    private float lastSecond; // Keep track of the last second

    private void Start()
    {
        // Update time and day on start
        UpdateTime();
        UpdateDay();
    }

    private void Update()
    {
        // Update time every frame
        UpdateTime();
    }

    private void UpdateTime()
    {
        // Get current time
        DateTime currentTime = DateTime.Now;

        // Update colon visibility every second
        if ((int)currentTime.Second != (int)lastSecond)
        {
            colonVisible = !colonVisible; // Toggle colon visibility
            lastSecond = currentTime.Second;

            // Display time in hh<colon>mm format
            timeText.text = currentTime.ToString($"hh{(colonVisible ? ":" : " ")}mm");
        }
    }

    private void UpdateDay()
    {
        // Get current day
        DateTime currentDate = DateTime.Now;

        // Display short day name (e.g., Mon, Tue, etc.)
        dayText.text = currentDate.ToString("ddd");
    }
}