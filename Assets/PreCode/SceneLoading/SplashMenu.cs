using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SplashMenu : MonoBehaviour
{
    [SerializeField]
    private FadePanelManager fadePanel;

    private string nextSceneName;
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField]
    private Image skipFiller;
    private bool loading = false;
    private bool isSkipping = false;
    [SerializeField]
    private float skipKeyHoldDuration = 1.2f;
    public float fadeOutDelay = 8f;
    public float skipTimer = 0f;
    private bool skipped = false;
    
    
    private void Awake()
    {
        this.fadePanel.OnFadeSequenceComplete += this.DisplaySplashScreen;
        
        videoPlayer.started += OnVideoStarted;

        string playerName = PlayerPrefs.GetString("username", string.Empty);

        if (playerName == string.Empty)
        {
            this.nextSceneName = "LoginScene";
        }
        else
        {
            this.nextSceneName = "JaredDev";
        }
    }

    void OnVideoStarted(VideoPlayer inVideoPlayer)
    {
        videoPlayer.started -= OnVideoStarted;
        this.fadePanel.FadeFromBlack();
    }
    
    private void DisplaySplashScreen()
    {
        this.fadePanel.OnFadeSequenceComplete -= this.DisplaySplashScreen;
        StartCoroutine(this.DisplayCoroutine());
    }

    private IEnumerator DisplayCoroutine()
    {
        yield return new WaitForSeconds(fadeOutDelay);
        SceneLoader.instance.LoadScene(this.nextSceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0))
        {
            isSkipping = true;
        }
        
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonUp(0))
        {
            skipFiller.fillAmount = 0;
            isSkipping = false;
            skipTimer = 0f;
        }

        if (isSkipping && !skipped)
        {
            skipTimer += Time.deltaTime;
            skipFiller.fillAmount = skipTimer / skipKeyHoldDuration;
            if (skipTimer > skipKeyHoldDuration)
            {
                SceneLoader.instance.LoadScene(this.nextSceneName);
                StartCoroutine(FadeOutVideoVolume());
                skipped = true;

            }
        }
    }

    public IEnumerator FadeOutVideoVolume()
    {
        AudioSource aSource = videoPlayer.GetTargetAudioSource(0);
        while (aSource.volume > 0f)
        {
            aSource.volume -= Time.deltaTime * 0.01f;
            yield return 0;
        }
    }
    
}
