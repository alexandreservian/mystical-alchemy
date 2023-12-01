using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{

    //Singleton
    
    public static Transition Instance { get; private set; }
    private Animator animationCanvas;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        animationCanvas = GetComponent<Animator>();
        SceneManager.sceneLoaded += LoadTransition;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= LoadTransition;
    }

    private void LoadTransition(UnityEngine.SceneManagement.Scene scene, LoadSceneMode sceneMode)
    {
        Time.timeScale = 1f;
        animationCanvas.SetBool("isFadeIn", true);
    }

    public IEnumerator LoadSceneDelayed(int sceneIndex)
    {
        animationCanvas.SetBool("isFadeIn", false);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(sceneIndex);
    }
}
