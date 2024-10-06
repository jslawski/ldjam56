using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField]
    private Transform playfieldTransform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bug")
        {
            this.gameObject.layer = LayerMask.NameToLayer("Playfield");
            this.gameObject.transform.parent = this.playfieldTransform;

            Scorekeeper.AddObject();

        }
    }
}
