using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenBug : MonoBehaviour
{
    private bool dying = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartDying",4f);
    }

    public void StartDying()
    {
        dying = true;
    }

    void Update()
    {

        if (transform.position.y < -4)
        {
            Destroy(gameObject);
        }

        if (!dying)
            return;
            
        transform.localScale -= Vector3.one * (0.2f * Time.deltaTime);
        if (transform.localScale.x < 0.01f)
        {
            Destroy(gameObject);
        }
    }
    
}
