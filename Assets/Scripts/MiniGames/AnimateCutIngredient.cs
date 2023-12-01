using UnityEngine;

public class AnimateCutIngredient : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void Animate()
    {
        animator.enabled = true;
    }
}
