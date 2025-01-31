using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionAnimator : MonoBehaviour
{
    public static TransitionAnimator Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        fadeImage.SetActive(true);
    }

    [SerializeField] GameObject fadeImage;
    public Animator transition;
    public bool isTransitioning;

    public void LoadGame(int levelIndex)
    {
        if(isTransitioning) {
            return;
        }

        isTransitioning = true;
        StartCoroutine(LoadLevel(levelIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("START");
        yield return new WaitForSeconds(1);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        isTransitioning = false;
        FadeIn();
    }

    public void FadeOut()
    {
        transition.SetTrigger("START");
    }

    public void FadeIn()
    {
        transition.SetTrigger("END");
    }
}
