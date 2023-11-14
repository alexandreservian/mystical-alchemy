using System;
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
    [SerializeField] private GameObject egg;
    [Header("Mini Game Settings:")]
    [SerializeField] [Range(3, 6)] private int numberOfHits = 3;
    [Header("Pivot Settings:")]
    [SerializeField] [Range(0.5f, 1.5f)] private float durationPivot = 0.5f;
    [Header("Slider Bar Settings:")]
    [SerializeField] [Range(0.2f, 1f)] private float durationSliderBar = 1f;
    private float limitIndicateBreakTop;
    private float limitIndicateBreakBottom;
    private float multipleScaleSliderBar;
    private float sizeSliderBar = 0;
    private int successHits = 0;
    private int failedAttempts = 0;
    public static event Action OnSuccessBreak;
    void Start()
    {
        var multipleScalar = (bar.bounds.size.y / sliderBarSprite.bounds.size.y) / numberOfHits;
        limitIndicateBreakTop = indicateBreak.bounds.max.y;
        limitIndicateBreakBottom = indicateBreak.bounds.min.y;
        multipleScaleSliderBar = Mathf.Floor(multipleScalar * 100.0f) / 100.0f;
        sliderBar.transform.localScale = new Vector3(1,0,0);
        var endPosition = bar.bounds.min.y + 0.11f;
        pivot.transform.DOMoveY(endPosition, durationPivot).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        sliderBar.transform.DOScaleY(sizeSliderBar, durationSliderBar);
    }

    private void Update() {
        var positionPivotY = pivot.transform.position.y;
        var isInsideLimit = positionPivotY <= limitIndicateBreakTop && positionPivotY >= limitIndicateBreakBottom;
        var limitHits = successHits != numberOfHits;
        if (InputManager.instance.isPressedTouch && limitHits) {
            if(isInsideLimit) {
                var newScale = sliderBar.transform.localScale.y + multipleScaleSliderBar;
                sliderBar.transform.DOScaleY(newScale, durationSliderBar);
                successHits++;
                if(successHits<numberOfHits){
                }
                egg.transform.DOMoveX(1.3f, 0.35f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
            }else{
                sliderBar.transform.DOScaleY(0, durationSliderBar);
                successHits = 0;
                MiniGamesManager.OnFail?.Invoke();
            }
            if(successHits==numberOfHits){
                OnSuccessBreak.Invoke();
            }
        }
    }

    public void NextStep() {
        transform.DOMoveX(-9f, 0.5f).SetEase(Ease.OutSine).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
}
