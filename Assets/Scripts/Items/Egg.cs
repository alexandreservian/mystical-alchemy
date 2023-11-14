using System.Collections;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector3 _dragOffSet;
    void Awake() {
        _mainCamera = Camera.main;    
    }

    void OnMouseDown()
    {
        _dragOffSet = transform.position - GetPosition();
    }

    void Update()
    {
        if(InputManager.instance.isPressedTouch) {
            if(IsClickedOn()){
                StartCoroutine(onDrag());
            }
        } else {
            onDrop();
        }
    }

    bool IsClickedOn() {
		RaycastHit2D hit = Physics2D.Raycast(GetPosition(), Vector2.zero);
		return hit.collider != null && hit.collider.gameObject.name == "Egg" ? true : false; 
    }

    Vector3 GetPosition() {
        var position = _mainCamera.ScreenToWorldPoint(InputManager.instance.positionTouch);
        return position;
    }

    IEnumerator onDrag() {
        _isDragging = true;

        while(_isDragging)
		{
			transform.position = GetPosition() + _dragOffSet;
			yield return null;
		}
        
    }

    void onDrop() {
        _isDragging = false;
    }
}
