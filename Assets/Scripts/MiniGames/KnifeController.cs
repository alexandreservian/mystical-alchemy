using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KnifeController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private RectTransform knifeRectTransform;
    [SerializeField] private RectTransform knifeShadowRectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private GraphicRaycaster graphicRaycaster;

    [SerializeField] private List<GameObject> cutLines = new List<GameObject>();

    [SerializeField] private PointerEventData pointerEventData;
    [SerializeField] private EventSystem eventSystem;

    [SerializeField] private Sprite normalKnife;
    [SerializeField] private Sprite highlightedKnife;

    [SerializeField] private Image knifeSprite;

    private Animator knifeAnimator;
    [SerializeField] private Animator ingredientAnimator;

    private int successCount = 0;
    private int failCount = 0;

    void Start()
    {
        eventSystem = GetComponent<EventSystem>();
        knifeAnimator = GetComponent<Animator>();

        DisableCutLines();
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
        knifeAnimator.SetBool("isGrabbed", true);
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(knifeRectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition);

        Vector3 newPosition = knifeRectTransform.localPosition + new Vector3(localPointerPosition.x, localPointerPosition.y, 0);
        knifeRectTransform.localPosition = newPosition;
        knifeShadowRectTransform.localPosition = newPosition;
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
            
            successCount = 0;
            failCount = 0;

            // Iterate through the results and handle interactions with UI elements
            foreach (RaycastResult result in results)
            {
                // Perform actions based on the UI element hit
                GameObject hitObject = result.gameObject;
                Debug.Log("UI Element Hit: " + hitObject.name);

                if (result.gameObject.CompareTag("Success"))
                {
                    successCount++;
                    cutLines.Remove(result.gameObject);
                    result.gameObject.GetComponent<AnimateCutIngredient>()?.Animate();
                    result.gameObject.SetActive(false);
                    MiniGamesManager.OnMiniGameSuccess.Invoke();
                    SoundManager.Instance.PlaySfx("Knife Cut");
                }
                else if (result.gameObject.CompareTag("Fail")) 
                {
                    failCount++;
                }
            }

            if (failCount > 0 && successCount <= 0)
            {
                MiniGamesManager.OnFail?.Invoke();
            }

            if (cutLines.Count <= 0)
            {
                if(ingredientAnimator != null) ingredientAnimator.enabled = true;
                enabled = false;
                MiniGamesManager.OnSuccess.Invoke();
            }

            DisableCutLines();
        }

        // Set canvasGroup.blocksRaycasts to true if needed
        canvasGroup.blocksRaycasts = true;

        knifeAnimator.SetBool("isGrabbed", false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    private void EnableCutLines()
    {
        foreach (GameObject cutLine in cutLines)
        {
            if (cutLine != null) cutLine.SetActive(true);
        }
    }

    private void DisableCutLines()
    {
        foreach (GameObject cutLine in cutLines)
        {
            if (cutLine != null) cutLine.SetActive(false);
        }
    }

}