using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotManager : MonoBehaviour
{
    private Camera cam;

    [SerializeField]
    private Camera screenshotCam;
    private Camera parentCamContainer;
    public RenderTexture lastScreenshotTaken;
    public RawImage targetImage;
    public static Texture2D screenshotTexture;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        PlayfieldManager.OnPanicStarted += JustPaniced;
    }

    private void OnDestroy()
    {
        PlayfieldManager.OnPanicStarted -= JustPaniced;
    }

    void JustPaniced()
    {
        StartCoroutine(TakeScreenshot(0.5f));
    }

    IEnumerator TakeScreenshot(float delay=0.1f)
    {
        screenshotCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        yield return new WaitForEndOfFrame();
        /*
        RenderTexture currentRT = RenderTexture.GetTemporary(screenshotCam.pixelWidth, screenshotCam.pixelHeight, 24);
        screenshotCam.targetTexture = currentRT;
        screenshotCam.Render();
        RenderTexture previousRT = RenderTexture.active;
        RenderTexture.active = currentRT;
        */
        RenderTexture scaledTexture = RenderTexture.GetTemporary(Screen.width, Screen.height);
        Graphics.Blit(lastScreenshotTaken, scaledTexture);
        screenshotTexture = new Texture2D(scaledTexture.width, scaledTexture.height, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, 0, scaledTexture.width, scaledTexture.height), 0, 0);
        screenshotTexture.Apply();
        RenderTexture.ReleaseTemporary(scaledTexture);
        targetImage.texture = screenshotTexture;
        screenshotCam.gameObject.SetActive(false);
    }

    public void TakeImmediateScreenshot()
    {
        StartCoroutine(TakeScreenshot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
