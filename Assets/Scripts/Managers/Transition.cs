using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{

    //Singleton
    
    public static Transition Instance { get; private set; }
    private Animator animationCanvas;

    [SerializeField] private Image backgroundImage;


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
        StartCoroutine(LoadTransitionDelayed());
    }

    public IEnumerator LoadTransitionDelayed()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        backgroundImage.DOFade(0f, 0.5f).SetUpdate(true);
    }

    public IEnumerator LoadSceneDelayed(int sceneIndex)
    {
        backgroundImage.DOFade(1f, 0.5f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(sceneIndex);
    }
}
