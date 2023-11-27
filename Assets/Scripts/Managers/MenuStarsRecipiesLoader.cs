using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class MenuStarsRecipiesLoader : MonoBehaviour
{
    private Image[] currentStarSprites;

    private void Awake()
    {
        currentStarSprites = GetComponentsInChildren<Image>();
    }

    public void SetStarsSprite(Sprite goldenStar, int stars = 0)
    {
        for(int i = 1; i <= stars; i++)
        {
            currentStarSprites[i].sprite = goldenStar;
        }
    }
}
