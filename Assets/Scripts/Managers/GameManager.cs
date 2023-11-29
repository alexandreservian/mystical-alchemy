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

    public void RestartStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToRecipiesMenu() {
        SceneManager.LoadScene(3);
    }

    public void RecipeOne() {
        SceneManager.LoadScene(4);
    }

    public void NextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToCredit() {
        SceneManager.LoadScene(2);
    }

    public void GoToMenu() {
        SceneManager.LoadScene(0);
    }

}
