using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXTestScript : MonoBehaviour
{
    public List<Transform> testTransforms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Transform t = testTransforms[Random.Range(0, testTransforms.Count)];
            VFXManager.instance.PlayVFXAttached(VFXType.FOOD_GRAB, t, 1f);
        }
    }
}
