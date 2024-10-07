using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI bugCountLabel;
    [SerializeField]
    private TextMeshProUGUI bugPointValueLabel;
    [SerializeField]
    private TextMeshProUGUI bugScoreLabel;
    [SerializeField]
    private TextMeshProUGUI foodCountLabel;
    [SerializeField]
    private TextMeshProUGUI foodPointValueLabel;
    [SerializeField]
    private TextMeshProUGUI foodScoreLabel;
    [SerializeField]
    private TextMeshProUGUI totalScoreLabel;

    [SerializeField]
    private GameObject leaderboard;

    [SerializeField]
    private AudioClip gameOverAudio;

    private void OnEnable()
    {
        //AudioChannelSettings channelSettings = new AudioChannelSettings(false, 1.0f, 1.0f, 1.0f, "SFX");
        //AudioManager.instance.Play(this.gameOverAudio, channelSettings);

        this.bugCountLabel.text = Scorekeeper.GetHighestBugCount().ToString();
        this.bugPointValueLabel.text = Scorekeeper.bugPointValue.ToString();
        this.bugScoreLabel.text = Scorekeeper.GetBugScore().ToString();

        this.foodCountLabel.text = Scorekeeper.GetObjectCount().ToString();
        this.foodPointValueLabel.text = Scorekeeper.objectPointValue.ToString();
        this.foodScoreLabel.text = Scorekeeper.GetObjectScore().ToString();

        this.totalScoreLabel.text = Scorekeeper.GetTotalScore().ToString();
    }

    public void OnReplayPressed()
    {
        SceneLoader.instance.LoadScene("GameScene");
    }

    public void OnMenuPressed()
    {
        SceneLoader.instance.LoadScene("TitleScreen");
    }

    public void OnLeaderboardButtonPressed()
    {
        LeaderboardManager.instance.DisplayLeaderboard();
    }
}
