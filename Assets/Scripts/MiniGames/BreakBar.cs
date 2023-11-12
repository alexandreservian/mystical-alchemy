using DG.Tweening;
using UnityEngine;

public class BreakBar : MonoBehaviour
{
    [Header("Game Objects:")]
    [SerializeField] private SpriteRenderer bar;
    [SerializeField] private SpriteRenderer indicateBreak;
    [SerializeField] private GameObject pivot;
    [SerializeField] private GameObject sliderBar;
    [SerializeField] private SpriteRenderer sliderBarSprite;
    [Header("Mini Game Settings:")]
    [SerializeField] [Range(3, 6)] private int numberOfHits = 3;
    [Header("Pivot Settings:")]
    [SerializeField] [Range(0.5f, 1.5f)] private float duration = 6f;
    private float limitIndicateBreakTop;
    private float limitIndicateBreakBottom;
    private float multipleScaleSliderBar;
    void Start()
    {
        limitIndicateBreakTop = indicateBreak.bounds.max.y;
        limitIndicateBreakBottom = indicateBreak.bounds.min.y;
        multipleScaleSliderBar = (bar.bounds.size.y / sliderBarSprite.bounds.size.y) / numberOfHits;
        //sliderBar.transform.localScale = new Vector3(1,0,0);
        var endPosition = bar.bounds.min.y + 0.11f;
        pivot.transform.DOMoveY(endPosition, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void Update() {
        var positionPivotY = pivot.transform.position.y;
        var isInsideLimit = positionPivotY <= limitIndicateBreakTop && positionPivotY >= limitIndicateBreakBottom;
        if (InputManager.instance.isPressedTouch && isInsideLimit) {
            var newScale = sliderBar.transform.localScale.y + multipleScaleSliderBar;
            sliderBar.transform.DOScaleY(newScale, 1);
        }
    }
}
