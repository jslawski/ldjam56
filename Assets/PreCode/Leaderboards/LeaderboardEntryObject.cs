using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class LeaderboardEntryObject : MonoBehaviour
{
    public Image backgroundImage;
    public TextMeshProUGUI placementText;
    public TextMeshProUGUI username;    
    public TextMeshProUGUI scoreText;
    
    private void Start()
    {
        if (this.scoreText.text == "0")
        {
            this.gameObject.SetActive(false);
        }
    }

    public void UpdateEntry(string username, float score, float placement)
    {
        this.gameObject.SetActive(true);
        
        if (username == "" || score == 0)
        {            
            return;
        }
        

        this.username.text = username;
        this.scoreText.text = Mathf.RoundToInt(score).ToString();
    }    
}
