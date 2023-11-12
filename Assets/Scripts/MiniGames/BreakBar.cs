using DG.Tweening;
using UnityEngine;

public class BreakBar : MonoBehaviour
{
    [Header("Game Objects:")]
    [SerializeField] private SpriteRenderer bar;
    [SerializeField] private GameObject indicateBreak;
    [SerializeField] private GameObject pivot;
    [Header("Pivot Settings:")]
    [SerializeField] [Range(0.5f, 1.5f)] private float duration = 6f;
    void Start()
    {
        var endPosition = bar.bounds.min.y + 0.11f;
        pivot.transform.DOMoveY(endPosition, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
