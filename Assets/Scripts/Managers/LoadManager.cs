using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private Sprite goldenStar;
    [SerializeField] private List<MenuStarsRecipiesLoader> recipiesList;


    private void Start()
    {
        LoadRecipiesStars();
    }

    private void LoadRecipiesStars()
    {
        for(int i = 0; i < recipiesList.Count; i++)
        {
            recipiesList[i].SetStarsSprite(goldenStar, PlayerPrefs.GetInt($"Level{i+1}StarsCount", 0)); 
        }
    }
}
