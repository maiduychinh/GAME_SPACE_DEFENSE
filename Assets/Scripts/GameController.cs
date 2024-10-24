﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int CurrentID = 1;

    private GameObject currentLevel;
    public Spawner SpawnerCurrent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadLevel(int id)
    {
      
        string levelPath = "Level/Level" + id;

        DestroyLevel();

        GameObject levelPrefab = Resources.Load<GameObject>(levelPath);

        if (levelPrefab != null)
        {
           
            currentLevel = Instantiate(levelPrefab);
            currentLevel.name = "Level" + id;

            if (currentLevel.GetComponent<Spawner>() != null)
            {
                SpawnerCurrent = currentLevel.GetComponent<Spawner>();
            }
            else
            {
                SpawnerCurrent = null;
            }
        }
    }

    public void DestroyLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel); 
            currentLevel = null;   
            Debug.Log("Destroyed current level");

        }
        Resources.UnloadUnusedAssets(); 
    }

    public void DoWin()
    {
        UiController.instance.pauseWin.OnOpen();
    }
    public void DoLevel()
    {
        PauseGame();
        UiController.instance.PauseLevel.RandomlySelectSkills();
        UiController.instance.PauseLevel.OnOpen();



    }

    public void NextData()
    {
        CurrentID++;
    }

    public void NextLevel()
    {
        LoadLevel(CurrentID);
    }

    public void PauseGame()
    {
        // Dừng game
        Time.timeScale = 0;
        Debug.Log("Game Pause.");
    }
    public void ContinueGame()
    {
        // Tiếp tục game
        Time.timeScale = 1;
        Debug.Log("Game Resumed.");
    }

    public void DoOver()
    {
        UiController.instance.gameOver.OnOpen();
    }



}
