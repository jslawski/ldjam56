using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodKillPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private AudioClip splashAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sandwich")
        {
            this.gameOverPanel.SetActive(true);
            
            LeaderboardManager.instance.QueueLeaderboardUpdate(PlayerPrefs.GetString("username", "--"), Scorekeeper.GetTotalScore(), "leaderboard");

            AudioChannelSettings channelSettings = new AudioChannelSettings(false, 1.0f, 1.0f, 1.0f, "SFX");
            AudioManager.instance.Play(this.splashAudio, channelSettings);
        }

        Destroy(other.gameObject);
    }
}