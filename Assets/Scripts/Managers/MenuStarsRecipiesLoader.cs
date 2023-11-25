using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class MenuStarsRecipiesLoader : MonoBehaviour
{
    private List<Image> currentStarSprites = new();

    [SerializeField] private Sprite goldenStar; 

    public void SetStarsSprite(int stars = 0)
    {
        for(int i = 0; i <= stars; i++)
        {
            currentStarSprites[0].sprite = goldenStar;
        }
    }
}
