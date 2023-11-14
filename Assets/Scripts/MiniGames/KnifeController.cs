using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KnifeController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private GraphicRaycaster graphicRaycaster;

    [SerializeField] private List<GameObject> cutLines = new List<GameObject>();

    [SerializeField] private PointerEventData pointerEventData;
    [SerializeField] private EventSystem eventSystem;

    [SerializeField] private Sprite normalKnife;
    [SerializeField] private Sprite highlightedKnife;

    [SerializeField] private Image knifeSprite;

    [SerializeField] private Animator animator;


    public static Action OnCutSucceeded;

    private float successCount = 0;

    void Start()
    {
        eventSystem = GetComponent<EventSystem>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        canvasGroup.blocksRaycasts = false;
        knifeSprite.sprite = highlightedKnife;
        EnableCutLines();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        knifeSprite.sprite = normalKnife;

        // Ensure that the raycastOrigin is not null before proceeding
        if (raycastOrigin != null)
        {
            // Create the PointerEventData with null for the EventSystem
            pointerEventData = new PointerEventData(eventSystem);

            // Set the position of the PointerEventData to the position of the raycastOrigin
            pointerEventData.position = raycastOrigin.position;
            Debug.Log(pointerEventData.position);

            // Create a list to store the raycast results
            List<RaycastResult> results = new List<RaycastResult>();

            // Raycast using the GraphicRaycaster
            graphicRaycaster.Raycast(pointerEventData, results);

            // Iterate through the results and handle interactions with UI elements
            foreach (RaycastResult result in results)
            {
                // Perform actions based on the UI element hit
                GameObject hitObject = result.gameObject;
                Debug.Log("UI Element Hit: " + hitObject.name);

                if (result.gameObject.CompareTag("Success"))
                {
                    //Succeed objective
                    successCount++;
                    break;
                }
            }
            if(successCount > 0)
            {
                successCount = 0;

                animator.enabled = true;
                OnCutSucceeded?.Invoke();
                enabled = false;
                Debug.Log("Success");
            }
            else
            {
                MiniGamesManager.OnFail?.Invoke();
                Debug.Log("Fail");
            }
        }

        // Set canvasGroup.blocksRaycasts to true if needed
        canvasGroup.blocksRaycasts = true;

        // Call the DisableCutLines method if needed
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