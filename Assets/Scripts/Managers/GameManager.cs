using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartGame() {
        SceneManager.LoadScene(4);
    }

    public void RecipeOne() {
        SceneManager.LoadScene(3);
    }
}
