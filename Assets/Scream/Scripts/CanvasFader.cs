using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasFader : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration = 5f;
    public bool fadeBool = false;
    public string scenename;


    void Start()
    {
        if (fadeBool == true)
        {
            FadeIn();
        }
    }
    public void FadeIn()
    {
        StartCoroutine(FadeCanvas(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvas(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
    }

    private IEnumerator FadeCanvas(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime/duration);
            yield return null;
        }

        cg.alpha = end;
        if (fadeBool == false)
        {
            SceneManager.LoadScene(scenename);
        }
        
    }
}
