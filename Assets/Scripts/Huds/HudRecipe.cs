using UnityEngine;

public class HudRecipe : MonoBehaviour
{
    [SerializeField] RectTransform panel;

    public void OnShowRecipePanel() {
        panel.gameObject.SetActive(true);
        InputManager.PlayerInput.SwitchCurrentActionMap("Menu");
    }

    public void OnCloseRecipePanel() {
        panel.gameObject.SetActive(false);
        InputManager.PlayerInput.SwitchCurrentActionMap("Gameplay");
    }
}
