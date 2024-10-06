using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI bugCountLabel;
    [SerializeField]
    private TextMeshProUGUI objectCountLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.bugCountLabel.text = Scorekeeper.GetBugCount().ToString();
        this.objectCountLabel.text = Scorekeeper.GetObjectCount().ToString();
    }
}
