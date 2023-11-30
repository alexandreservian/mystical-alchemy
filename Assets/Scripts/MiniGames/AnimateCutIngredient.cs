using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCutIngredient : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void Animate()
    {
        animator.enabled = true;
    }
}
