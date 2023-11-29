using DG.Tweening;
using UnityEngine;

public class UiMenu : MonoBehaviour
{
    [Header("Plate Settings:")]
    [SerializeField] RectTransform plate;
    [SerializeField] [Range(0.2f, 1f)] float timeAnimation = 1f;
    [SerializeField] Ease ease = Ease.InQuad;
    [Header("Buttons Settings:")]
    [SerializeField] RectTransform buttonPlay;
    void Start()
    {
        plate.DOAnchorPosY(-219f, timeAnimation)
            .SetLoops(1, LoopType.Yoyo)
            .SetEase(ease)
            .OnComplete(() => {
                buttonPlay.DOScale(new Vector3(1,1,1), timeAnimation);
            });
    }
}
