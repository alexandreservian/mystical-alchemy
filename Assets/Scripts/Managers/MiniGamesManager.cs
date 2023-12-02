using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGamesManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Button nextMiniGameButton;
    [SerializeField] private GameObject nextMiniGameButtonBG;
    [SerializeField] private Button endStageButton;
    [SerializeField] private Button restartStageButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private GameObject endGameCanvas;
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject nextButton;
    
    [Header("MiniGames")]
    [SerializeField] private List<GameObject> miniGameList = new ();
    [SerializeField] private int twoStarsMaxErrors = 1;
    [SerializeField] private int threeStarsMaxErrors = 0;

    [Header("Endgame Score Canvas")]
    [SerializeField] private Sprite goldStar;
    [SerializeField] private Image twoStarsImage;
    [SerializeField] private Image threeStarsImage;
    [SerializeField] private float delayToShowScreenSeconds = 1f;

    [SerializeField] private Animation lightAnimation;
    //Events
    public static Action OnSuccess;
    public static Action OnMiniGameSuccess;
    public static Action OnFail;

    //Counters
    private int failCount = 0;
    private int successCount = 0;

    void Start()
    {
        OnFail = null;
        OnSuccess = null;
        OnMiniGameSuccess = null;

        OnFail += () =>
        {
            failCount++;
            lightAnimation.Play("anim_Light_Fail");
            SoundManager.Instance.PlaySfx("Fail Sound");
            Debug.Log($"Failed {failCount} times");
        };
        OnSuccess += () => SucceedMiniGame();

        OnMiniGameSuccess += () => lightAnimation.Play("anim_Light_Success");

        nextMiniGameButton.onClick.AddListener(() =>
        {
            miniGameList[successCount -1].SetActive(false);
            miniGameList[successCount].SetActive(true);
            nextMiniGameButtonBG.SetActive(false);
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

        miniGameList[0].SetActive(true);
    }

    private void SucceedMiniGame()
    {
        successCount++;
        if (miniGameList.Count <= successCount)
        {
            StartCoroutine(OnEndMiniGameDelayed(delayToShowScreenSeconds));
        }
        else
        {
            ProceedMiniGame();
        }
    }

    private IEnumerator OnEndMiniGameDelayed(float delayToShowScreenSeconds)
    {
        yield return new WaitForSecondsRealtime(delayToShowScreenSeconds);
        OnEndMiniGame();
    }

    void ProceedMiniGame()
    {
        nextMiniGameButton.gameObject.SetActive(true);
        nextMiniGameButtonBG.SetActive(true);
    }

    void OnEndMiniGame()
    {
        nextMiniGameButton.onClick.RemoveAllListeners();
        endGameCanvas.SetActive(true);
        hudCanvas.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            ShowCreditsButton();
        }

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
            if (!(PlayerPrefs.GetInt($"Level{SceneManager.GetActiveScene().buildIndex - 3}StarsCount", 0) > 2))
                SaveManager.SaveLevelsStars(SceneManager.GetActiveScene().buildIndex - 3, 2);
        }
        else
        {
            if (!(PlayerPrefs.GetInt($"Level{SceneManager.GetActiveScene().buildIndex - 3}StarsCount", 0) > 1))
                SaveManager.SaveLevelsStars(SceneManager.GetActiveScene().buildIndex - 3, 1);
        }
    }

    private void ShowCreditsButton()
    {
        Button nextButtonButton = nextButton.GetComponent<Button>();
        nextButtonButton.onClick.RemoveAllListeners();

        nextButtonButton.onClick.AddListener(GameManager.instance.GoToCredit);
    }
}
