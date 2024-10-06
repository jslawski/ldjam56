using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldAudio : MonoBehaviour
{
    private float screamAngleThreshold = 15.0f;
    private float panicAngleThreshold = 10.0f;

    [SerializeField]
    private AudioClip screamAudio;
    [SerializeField]
    private AudioClip calmMusic;
    [SerializeField]
    private AudioClip panicMusic;

    private Transform playfieldTransform;

    private int screamAudioChannelID = 0;
    private int calmMusicChannelID = 0;
    private int panicMusicChannelID = 0;

    private AudioChannelSettings screamChannelSettings;

    private bool isScreaming = false;

    private AudioChannelSettings musicOffSettings;
    private AudioChannelSettings musicOnSettings;

    private bool isPanic = false;

    private void Start()
    {
        this.playfieldTransform = GetComponent<Transform>();
        this.screamChannelSettings = new AudioChannelSettings(false, 1.0f, 1.0f, 0.5f, "SFX", this.gameObject.transform);
        this.musicOffSettings = new AudioChannelSettings(true, 1.0f, 1.0f, 0.0f, "BGM");
        this.musicOnSettings = new AudioChannelSettings(true, 1.0f, 1.0f, 1.0f, "BGM");

        this.calmMusicChannelID = AudioManager.instance.Play(this.calmMusic, this.musicOnSettings);
        this.panicMusicChannelID = AudioManager.instance.Play(this.panicMusic, this.musicOffSettings);
    }

    private void Update()
    {
        this.HandleScream();
        this.HandleMusic();
    }

    private void HandleScream()
    {
        if (this.IsPastScreamThreshold() == true && AudioManager.instance.IsPlaying(this.screamAudioChannelID) == false && this.isScreaming == false)
        {
            this.screamChannelSettings = new AudioChannelSettings(false, 1.0f, 1.0f, 0.5f, "SFX", this.gameObject.transform);
            this.screamAudioChannelID = AudioManager.instance.FadeIn(this.screamAudio, this.screamChannelSettings, 0.5f);
            this.isScreaming = true;
        }
        else if (this.IsPastScreamThreshold() == false && AudioManager.instance.IsPlaying(this.screamAudioChannelID) == true && this.isScreaming == true)
        {
            AudioManager.instance.FadeOut(this.screamAudioChannelID, 0.5f);
            this.isScreaming = false;
        }
    }

    private void HandleMusic()
    {
        if (this.IsPastPanicThreshold() == true && this.isPanic == false)
        {
            this.musicOffSettings = new AudioChannelSettings(true, 1.0f, 1.0f, 0.0f, "BGM");
            this.musicOnSettings = new AudioChannelSettings(true, 1.0f, 1.0f, 1.0f, "BGM");

            AudioManager.instance.FadeVolume(this.calmMusicChannelID, this.musicOnSettings.volume, this.musicOffSettings.volume, 1.0f);
            AudioManager.instance.FadeVolume(this.panicMusicChannelID, this.musicOffSettings.volume, this.musicOnSettings.volume, 1.0f);

            this.isPanic = true;
        }
        else if (this.IsPastPanicThreshold() == false && this.isPanic == true)
        {
            this.musicOffSettings = new AudioChannelSettings(true, 1.0f, 1.0f, 0.0f, "BGM");
            this.musicOnSettings = new AudioChannelSettings(true, 1.0f, 1.0f, 1.0f, "BGM");

            AudioManager.instance.FadeVolume(this.calmMusicChannelID, this.musicOffSettings.volume, this.musicOnSettings.volume, 1.0f);
            AudioManager.instance.FadeVolume(this.panicMusicChannelID, this.musicOnSettings.volume, this.musicOffSettings.volume, 1.0f);

            this.isPanic = false;
        }
    }

    private bool IsPastScreamThreshold()
    {
        return (Vector3.Angle(Vector3.up, this.gameObject.transform.up) >= this.screamAngleThreshold);
    }

    private bool IsPastPanicThreshold()
    {
        return (Vector3.Angle(Vector3.up, this.gameObject.transform.up) >= this.panicAngleThreshold);
    }
}
