using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] PlayerController playerController;
    [SerializeField] Counter counter;
    [SerializeField] int winCount = 70;
    [SerializeField] float winTimeInside = 1.0f;
    bool isGameActive;
    float timePassed;
    float timeInside;

    void Start()
    {
        // Video settings
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        StartCoroutine("GameStart");
    }

    void Update()
    {
        if (isGameActive)
        {
            timePassed += Time.deltaTime;
            UpdateTimer();
            CheckCounter();
        }
    }

    void UpdateTimer()
    {
        timerText.text = "Time: " + (TimeSpan.FromSeconds(timePassed)).ToString(@"mm\:ss");
    }

    void CheckCounter()
    {
        if (counter.Count >= winCount)
        {
            timeInside += Time.deltaTime;
            if (timeInside > winTimeInside)
            {
                WinGame();
            }
        }
        else
        {
            timeInside = 0;
        }
    }

    void WinGame()
    {
        isGameActive = false;
        winText.gameObject.SetActive(true);
        playerController.DisableInput();
        spawnManager.StopSpawner();
        spawnManager.FreezeItems();
    }

    IEnumerator GameStart()
    {
        startText.CrossFadeAlpha(0, 2, false);

        yield return new WaitForSeconds(2);

        playerController.EnableInput();
        startText.gameObject.SetActive(false);
        spawnManager.StartSpawner();
        isGameActive = true;
    }
}
