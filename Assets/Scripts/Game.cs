using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Tower _playerTower;
    [SerializeField] private Tower _enemyTower;
    [SerializeField] private WinGameScreen _winGameScreen;
    [SerializeField] private LoseGameScreen _loseGameScreen;

    private void OnEnable()
    {
        _winGameScreen.WinButtonClick += OnWinButtonClick;
        _loseGameScreen.MenuButtonClick += OnRestartButtonClick;
        _playerTower.GameOver += OnGameOver;
        _enemyTower.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _winGameScreen.WinButtonClick -= OnWinButtonClick;
        _loseGameScreen.MenuButtonClick -= OnRestartButtonClick;
        _playerTower.GameOver -= OnGameOver;
        _enemyTower.GameOver -= OnGameOver;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void OnWinButtonClick()
    {
        DataBase.Instance.AddMoney(500);
        DataBase.Instance.SaveGameMoney();
        UnlockNextLevel();
        DataBase.Instance.SaveGameLevel();
        SceneManager.LoadScene("menu");
    }

    private void UnlockNextLevel()
    {
        int positionNumber = SceneManager.GetActiveScene().name.LastIndexOf('l');
        string winLevel = SceneManager.GetActiveScene().name.Substring(positionNumber + 1);
        int numberLevel;

        if (ReadInt(winLevel, out numberLevel))
        {
            DataBase.Instance.PlayerLevels[numberLevel] = 5;
            if (numberLevel + 1 < DataBase.Instance.PlayerLevels.Length)
            {
                DataBase.Instance.PlayerLevels[numberLevel + 1] = 1;
            }
        }
        
    }

    public bool ReadInt(string winLevel, out int numberLevel)
    {
        return int.TryParse(winLevel, out numberLevel);
    }

    private void OnRestartButtonClick()
    {
        _loseGameScreen.Close();
        SceneManager.LoadScene("menu");
    }

    public void OnGameOver(bool isWinGame)
    {
        Time.timeScale = 0;
        if (isWinGame)
        {
            _winGameScreen.Open();
        }
        else
        {
            _loseGameScreen.Open();
        }    
    }
}
