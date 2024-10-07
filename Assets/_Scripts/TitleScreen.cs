using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TitleScreen : MonoBehaviour
{
    [Header("Camera Related")] 
    [SerializeField]
    private Transform camContainer;
    [SerializeField]
    private Transform cam;
    
    [Header("Ant Related")]
    [SerializeField] private Transform antRainer;
    [SerializeField] private Rigidbody antPrefab;
    [SerializeField] private AudioClip antStickSound;
    [SerializeField] private AudioClip titleBgm;

    [Header("UI")] 
    [SerializeField] private Image logo;
    [SerializeField] private Image logoBG;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    
    [Header("Orbiting")]
    public float orbitSpeed = 10f;
    public float minCamZoom = 2f;
    public float maxCamZoom = 10f;
    public float targetZoom = 2f;
    public float zoomSpeed = 1f;
    public float zoomChangeAverageDuration = 8f;
    
    private float zoomChangeTimer = 0f;

    public static bool sculptMode = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;    

        zoomChangeTimer = Random.Range(zoomChangeAverageDuration*0.75f, zoomChangeAverageDuration*1.5f);
        
        if (PlayerPrefs.HasKey("MusicVolume"))
        {   
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            musicSlider.value = 0.2f;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            sfxSlider.value = 0.85f;   
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        camContainer.transform.Rotate(Vector3.up,orbitSpeed*Time.deltaTime);

        if (zoomChangeTimer > 0f)
        {
            zoomChangeTimer -= Time.deltaTime;
            if (zoomChangeTimer <= 0)
            {
                targetZoom = Random.Range(minCamZoom, maxCamZoom);
                zoomChangeTimer = Random.Range(zoomChangeAverageDuration*0.75f, zoomChangeAverageDuration*1.5f);
            }
        }

        if (Mathf.Abs(cam.transform.localPosition.magnitude - targetZoom) > 0.1f)
        {
            float sign = Mathf.Sign(targetZoom-cam.transform.localPosition.magnitude);
            cam.transform.localPosition += cam.transform.localPosition.normalized * (sign*Time.deltaTime * zoomSpeed);
        }


        if (Random.Range(0, 100) < 25)
        {
            Rigidbody antBody = Instantiate(antPrefab, antRainer);
            antBody.transform.position = antRainer.position;
            antBody.transform.localEulerAngles = Random.insideUnitSphere * 360;
            antBody.AddForce((antRainer.forward*8f)+ Random.insideUnitSphere*2f, ForceMode.Impulse); 
        }
        
    }



    public void StartGame(bool inSculptMode)
    {
        sculptMode = inSculptMode;
        SceneLoader.instance.LoadScene("GameScene");
    }

    public void StartFreePlay(bool inSculptMode)
    {
        sculptMode = inSculptMode;
        SceneLoader.instance.LoadScene("FreePlayScene");
    }

    public void ShowLeaderboard()
    {
        // TODO(brainoid): Show leaderboard
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameSequence());
    }

    public IEnumerator QuitGameSequence()
    {
        AudioSource musicSource = GetComponent<AudioSource>();
        while (musicSource.pitch > -0.4f)
        {
            orbitSpeed -= 0.8f * Time.deltaTime;
            musicSource.pitch -= 0.5f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("QUIT!");
        Application.Quit();
    }


    public void GotoURl(string URL)
    {
        Application.OpenURL(URL);
    }
    
    
    public void UpdateMusicVolume(float ratio)
    {
        // set the music volume using logarithmic approach
        mixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(ratio) * 20);
        PlayerPrefs.SetFloat("MusicVolume",ratio);
    }
    
    public void UpdatSFXVolume(float ratio)
    {
        mixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(ratio) * 20);
        PlayerPrefs.SetFloat("SFXVolume",ratio);
    }
    
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bug"))
        {
            Destroy(other.rigidbody);
            other.transform.SetParent(transform,true);
            other.transform.position = other.GetContact(0).point;

            AudioChannelSettings musicChannelSettings = new AudioChannelSettings(false, 0.8f, 1.2f, 0.05f, "SFX");
            AudioManager.instance.Play(this.antStickSound, musicChannelSettings);
        }
    }
    
    
}
