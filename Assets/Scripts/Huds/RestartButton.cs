using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GameManager.instance.RestartStage());
    }

}
