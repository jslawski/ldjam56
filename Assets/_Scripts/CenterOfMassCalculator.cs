using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMassCalculator : MonoBehaviour
{
    [SerializeField]
    private Rigidbody cubeRigidbody;
    
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = this.cubeRigidbody.gameObject.transform.position + this.cubeRigidbody.centerOfMass;
    }
}
