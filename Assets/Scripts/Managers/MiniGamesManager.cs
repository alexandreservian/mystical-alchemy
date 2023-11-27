using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGamesManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Button nextMiniGameButton;
    [SerializeField] private Button endStageButton;
    [SerializeField] private Button restartStageButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private GameObject endGameCanvas;
    [SerializeField] private GameObject hudCanvas;
    
    [Header("MiniGames")]
    [SerializeField] private List<GameObject> miniGameList = new ();
    [SerializeField] private int twoStarsMaxErrors = 1;
    [SerializeField] private int threeStarsMaxErrors = 0;

    [Header("Endgame Score")]
    [SerializeField] private Sprite goldStar;
    [SerializeField] private Image twoStarsImage;
    [SerializeField] private Image threeStarsImage;

    //Events
    public static Action OnSuccess;
    public static Action OnFail;

    //Counters
    private int failCount = 0;
    private int successCount = 0;

    void Start()
    {
        OnFail = null;
        OnSuccess = null;

        OnFail += () =>
        {
            failCount++;
            Debug.Log($"Failed {failCount} times");
        };
        OnSuccess += () => SucceedMiniGame();

        nextMiniGameButton.onClick.AddListener(() =>
        {
            miniGameList[successCount -1].SetActive(false);
            miniGameList[successCount].SetActive(true);
            nextMiniGameButton.gameObject.SetActive(false);
        });

        restartStageButton.onClick.AddListener(() =>
        {
            GameManager.instance.RestartStage();
        });

        menuButton.onClick.AddListener(() =>
        {
            GameManager.instance.GoToRecipiesMenu();
        });

        endStageButton.onClick.AddListener(() => GameManager.instance.GoToRecipiesMenu());
    }

    private void SucceedMiniGame()
    {
        successCount++;
        if (miniGameList.Count <= successCount)
        {
            OnEndMiniGame();
            return;
        }
        ProceedMiniGame();
    }

    void ProceedMiniGame()
    {
        nextMiniGameButton.gameObject.SetActive(true);
    }

    void OnEndMiniGame()
    {
        nextMiniGameButton.onClick.RemoveAllListeners();
        endGameCanvas.SetActive(true);
        hudCanvas.SetActive(false);
        OnFail = null;
        OnSuccess = null;
        
        if (failCount <= threeStarsMaxErrors)
        {
            twoStarsImage.sprite = goldStar;
            threeStarsImage.sprite = goldStar;
            SaveManager.SaveLevelsStars(SceneManager.GetActiveScene().buildIndex - 3, 3);
        }
        else if (failCount <= twoStarsMaxErrors)
        {
            twoStarsImage.sprite = goldStar;
            if (PlayerPrefs.GetInt($"Level{SceneManager.GetActiveScene().buildIndex - 3}StarsCount", 0) > 2)
                return;
            SaveManager.SaveLevelsStars(SceneManager.GetActiveScene().buildIndex - 3, 2);
        }
        else
        {
            if (PlayerPrefs.GetInt($"Level{SceneManager.GetActiveScene().buildIndex - 3}StarsCount", 0) > 1)
                return;
            SaveManager.SaveLevelsStars(SceneManager.GetActiveScene().buildIndex - 3, 1);
        }
    }

}
