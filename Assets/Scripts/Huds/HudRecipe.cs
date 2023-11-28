using UnityEngine;

public class HudRecipe : MonoBehaviour
{
    [SerializeField] RectTransform panel;

    private void Start() {
        OnShowRecipePanel();
    }

    public void OnShowRecipePanel() {
        Time.timeScale = 0;
        panel.gameObject.SetActive(true);
        InputManager.PlayerInput.SwitchCurrentActionMap("Menu");
    }

    public void OnCloseRecipePanel() {
        Time.timeScale = 1;
        panel.gameObject.SetActive(false);
        InputManager.PlayerInput.SwitchCurrentActionMap("Gameplay");
    }
}
