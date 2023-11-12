using DG.Tweening;
using UnityEngine;

public class BreakBar : MonoBehaviour
{
    [Header("Pivot Settings:")]
    [SerializeField] private GameObject pivot;
    [SerializeField] [Range(0.5f, 1.5f)] private float duration = 6f;
    void Start()
    {
        pivot.transform.DOMoveY(-0.75f, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
