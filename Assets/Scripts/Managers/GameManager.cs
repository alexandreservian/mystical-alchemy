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
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void RestartStage()
    {
        StartCoroutine(Transition.Instance.LoadSceneDelayed(SceneManager.GetActiveScene().buildIndex));
    }

    public void GoToRecipiesMenu() {
        StartCoroutine(Transition.Instance.LoadSceneDelayed(3));
    }

    public void RecipeOne() {
        StartCoroutine(Transition.Instance.LoadSceneDelayed(4));
    }

    public void RecipeTwo()
    {
        StartCoroutine(Transition.Instance.LoadSceneDelayed(5));
    }

    public void NextStage()
    {
        StartCoroutine(Transition.Instance.LoadSceneDelayed(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoToCredit() {
        StartCoroutine(Transition.Instance.LoadSceneDelayed(2));
    }

    public void GoToMenu() {
        StartCoroutine(Transition.Instance.LoadSceneDelayed(0));
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(Transition.Instance.LoadSceneDelayed(0));
    }

}
