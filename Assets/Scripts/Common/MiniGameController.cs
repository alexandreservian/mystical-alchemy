using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameController : MonoBehaviour
{
    //public Action OnMiniGameStarted;
    //public Action OnMiniGameFinished;
    //public Action OnSuccess;
    //public Action OnFailure;

    //private int failCounts;

    [SerializeField] private Button nextButton;

    //Singleton
    public static MiniGameController Instance;

    public void Awake()
    {
        Instance = this;

        if(Instance != null)
        {
            Destroy(Instance.gameObject);
        }
    }

}
