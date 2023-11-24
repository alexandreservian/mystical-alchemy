using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextStageButton : MonoBehaviour
{

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GameManager.instance.NextStage());
    }

}
