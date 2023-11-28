using System;
using System.Collections;
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
    [Header("Egg Settings:")]
    [SerializeField] private GameObject egg;
    [SerializeField] private SpriteRenderer spriteEgg;
    [SerializeField] private Sprite[] stepsEgg;
    [SerializeField] private Sprite stepsEggBreak;
    private Sprite cacheSpriteEgg;
    [Header("Mini Game Settings:")]
    [SerializeField] [Range(3, 6)] private int numberOfHits = 3;
    [Header("Pivot Settings:")]
    [SerializeField] [Range(0.5f, 1.5f)] private float durationPivot = 0.5f;
    [Header("Slider Bar Settings:")]
    [SerializeField] [Range(0.2f, 1f)] private float durationSliderBar = 1f;
    [Header("Timer Settings:")]
    [SerializeField] [Range(0.2f, 1f)] private float limitTimer = 1.0f;
    private bool canDoAction = true;
    private float limitIndicateBreakTop;
    private float limitIndicateBreakBottom;
    private float multipleScaleSliderBar;
    private float sizeSliderBar = 0;
    private int successHits = 0;

    void Start()
    {
        var multipleScalar = (bar.bounds.size.y / sliderBarSprite.bounds.size.y) / numberOfHits;
        limitIndicateBreakTop = indicateBreak.bounds.max.y;
        limitIndicateBreakBottom = indicateBreak.bounds.min.y;
        multipleScaleSliderBar = Mathf.Floor(multipleScalar * 100.0f) / 100.0f;
        sliderBar.transform.localScale = new Vector3(sliderBar.transform.localScale.x,0,0);
        var endPosition = bar.bounds.min.y + 0.11f;
        pivot.transform.DOMoveY(endPosition, durationPivot).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        sliderBar.transform.DOScaleY(sizeSliderBar, durationSliderBar);
        cacheSpriteEgg = spriteEgg.sprite;
    }

    private void Update() {
        var positionPivotY = pivot.transform.position.y;
        var isInsideLimit = positionPivotY <= limitIndicateBreakTop && positionPivotY >= limitIndicateBreakBottom;
        var limitHits = successHits != numberOfHits;
        if (successHits > numberOfHits) return;
        if (InputManager.instance.isPressedTouch && limitHits) {
            if(isInsideLimit && canDoAction) {
                var newScale = sliderBar.transform.localScale.y + multipleScaleSliderBar;
                sliderBar.transform.DOScaleY(newScale, durationSliderBar);
                successHits++;
                egg.transform.DOMoveX(0.2f, 0.35f)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine)
                    .OnStepComplete(() => {
                        spriteEgg.sprite = stepsEgg.Length == numberOfHits ? stepsEgg[successHits] : spriteEgg.sprite;
                    });
                SoundManager.Instance.PlaySfx("Egg Broken");
                StartCoroutine(WaitToBreakEgg());
            }else{
                sliderBar.transform.DOScaleY(0, durationSliderBar);
                spriteEgg.sprite = cacheSpriteEgg;
                successHits = 0;
                MiniGamesManager.OnFail?.Invoke();
            }
            if(successHits==numberOfHits){
                egg.transform.DOKill();
                egg.transform.DOMove(new Vector2(1.2f, -2f), 0.35f)
                    .SetLoops(1)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() => {
                        spriteEgg.sprite = stepsEggBreak;
                        SoundManager.Instance.PlaySfx("Finish Egg Broken");
                        MiniGamesManager.OnSuccess.Invoke();
                    });
            }
        }
    }

    IEnumerator WaitToBreakEgg() {
        canDoAction = false;
        yield return new WaitForSeconds(limitTimer);
        canDoAction = true;
    }

    public void NextStep() {
        transform.DOMoveX(-9f, 0.5f).SetEase(Ease.OutSine).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
}
