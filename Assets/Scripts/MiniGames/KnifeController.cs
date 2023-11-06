using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnifeController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform raycastOrigin;

    [SerializeField] private List<GameObject> cutLines = new List<GameObject>();

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        canvasGroup.blocksRaycasts = false;
        EnableCutLines();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");

        // Ensure that the raycastOrigin is not null before creating the ray
        if (raycastOrigin != null)
        {
            // Create a ray from the specified transform position
            Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);

            // Create a raycast hit variable to store information about the hit
            RaycastHit hit;

            // Check if the ray hits any UI element
            if (Physics.Raycast(ray, out hit))
            {
                // Perform actions based on the UI element hit
                if (hit.collider != null)
                {
                    // If a UI element is hit, you can get its information and perform necessary actions
                    Debug.Log("UI Element Hit: " + hit.collider.gameObject.name);

                    // Add your specific actions here for when a UI element is hit
                }
            }
        }

        canvasGroup.blocksRaycasts = true;
        DisableCutLines();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
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