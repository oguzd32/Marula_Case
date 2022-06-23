using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UnityEvent OnLevelStarted;

    [Header("Gameobject References")] 
    [SerializeField] private GameObject startUI = default;
    [SerializeField] private GameObject inGameUI = default;
    [SerializeField] private GameObject winUI = default;
    [SerializeField] private GameObject failUI = default;

    [Header("Text References")] 
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI endUILevelText;
    [SerializeField] private TextMeshProUGUI coinText;

    // private variables
    private int currentLevel = 0;
    private int currentCoinCount = 0;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Level", 0);
        currentCoinCount = PlayerPrefs.GetInt("Coin", 0);
        levelText.text = $"LEVEL {currentLevel + 1}";
    }

    #region Main Functions
    
    public void OnPlayerStartedLevel()
    {
        startUI.SetActive(false);
        inGameUI.SetActive(true);
        OnLevelStarted?.Invoke();
    }

    public void OnPlayerCompletedLevel()
    {
        
    }

    public void OnPlayerFailedLevel()
    {
        
    }
    
    #endregion
    
    #region Button Functions

    public void OnPlayButtonClicked()
    {
        OnPlayerStartedLevel();
    }

    public void OnRetryButtonClicked()
    {
        LevelLoader.Instance.NextLevel();
    }

    public void OnNextButtonClicked()
    {
        LevelLoader.Instance.RestartLevel();
    }
    
    #endregion
}
