using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGamesManager : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject breakMinigame;
    [SerializeField] private GameObject cutMinigame;

    public static Action OnFail;

    private int failCount = 0;

    void Start()
    {
        BreakBar.OnSuccessBreak += OnSuccessBreak;
        KnifeController.OnCutSucceeded += OnSuccessCut;
        OnFail += () => failCount++;
    }

    void OnSuccessBreak()
    {
        BreakBar.OnSuccessBreak -= OnSuccessBreak;
        nextButton.gameObject.SetActive(true);
        nextButton.onClick.AddListener(() =>
        {
            breakMinigame.SetActive(false);
            cutMinigame.SetActive(true);
            nextButton.gameObject.SetActive(false);
        });
    }

    void OnSuccessCut()
    {
        KnifeController.OnCutSucceeded -= OnSuccessCut;
        nextButton.gameObject.SetActive(true);
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() =>
        {
            GameManager.instance.StartGame();
            Debug.Log($"Game Succeeded with {failCount} fails");
            // End of the level
        });
    }

}
