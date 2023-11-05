using UnityEngine;

public class Egg : MonoBehaviour
{
    void Update()
    {
        Debug.Log(InputManager.instance.positionTouch);
    }
}
