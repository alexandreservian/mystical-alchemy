using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{

    [SerializeField] private List<MenuStarsRecipiesLoader> recipiesList = new();


    private void Start()
    {
        LoadRecipiesStars();
    }

    private void LoadRecipiesStars()
    {
        for(int i = 0; i < recipiesList.Count; i++)
        {
            recipiesList[i].SetStarsSprite(PlayerPrefs.GetInt($"Level{i+1}StarsCount", 0));
        }
    }
}
