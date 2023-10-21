using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnifeController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    [SerializeField] private List<GameObject> cutLines = new List<GameObject>();

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        canvasGroup.blocksRaycasts = false;
        EnableCutLines();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        canvasGroup.blocksRaycasts = true;
        DisableCutLines();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    private void EnableCutLines()
    {
        foreach (GameObject cutLine in cutLines)
        {
            if (cutLine == null) return;

            cutLine.SetActive(true);
        }
    }

    private void DisableCutLines()
    {
        foreach (GameObject cutLine in cutLines)
        {
            if (cutLine == null) return;
            cutLine.SetActive(false);
        }
    }

}