using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class Prolog : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer videoPlayer;
    public string gameplayScene = "Gameplay"; 

    // fade
    public Image fadeImage;
    public float fadeDuration = 1.5f;

    void Start()
    {
        StartCoroutine(FadeIn());
        // cek video selesai
        Debug.Log("1. Script Start Berjalan, Memulai Fade In...");

        Color awal = fadeImage.color;
        awal.a = 1f;
        fadeImage.color = awal;

        videoPlayer.loopPointReached += EndedVideo;

        
    }

    void EndedVideo(VideoPlayer vp)
    {
        Debug.Log("1. Script Start Berjalan, Memulai Fade Out...");
        StartCoroutine(FadeOut());

    }

    // kalau mau skip prolog
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0))
        //{
        //    SceneManager.LoadScene(namaSceneGameplay);
        //}
    }

     IEnumerator FadeIn()
    {
        float timer = 0f;
        Color camColor = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            
            camColor.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = camColor;

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float timer = 0f;
        Color camColor = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            
            camColor.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = camColor;
            yield return null;

            // pindah scene gameplay
            
        }
        SceneManager.LoadScene(gameplayScene);
    }
}
