using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static void SaveLevelsStars(int levelIndex, int starsCount)
    {
        PlayerPrefs.SetInt($"Level{levelIndex}StarsCount",starsCount);
    }
}
