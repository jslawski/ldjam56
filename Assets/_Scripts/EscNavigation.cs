using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscNavigation : MonoBehaviour
{
    [SerializeField]
    private string optionalSceneName;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (this.optionalSceneName != string.Empty)
            {
                SceneLoader.instance.LoadScene(this.optionalSceneName);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
