using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGamesManager : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    void Start()
    {
        BreakBar.OnSuccessBreak += OnSuccessBreak;
    }

    void OnSuccessBreak() {
        nextButton.gameObject.SetActive(true);
    }

    
}
