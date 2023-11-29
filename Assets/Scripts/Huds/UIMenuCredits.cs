using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIMenuCredits : MonoBehaviour
{
    [SerializeField] List<RectTransform> members;
    [SerializeField] RectTransform buttonBackMenu;
    void Start()
    {
        var sequence = DOTween.Sequence();

        foreach (var member in members) {
            sequence.Append(member.DOAnchorPosX(151.7f,0.3f)).SetEase(Ease.InQuad);
        }

        sequence.OnComplete(() => {
            buttonBackMenu.DOScale(new Vector3(1,1,1), 0.5f);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
