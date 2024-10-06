using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{    
    public Transform playfieldTransform;

    private float fallSpeed = 0.5f;

    private bool grabbed = false;

    [SerializeField]
    private GameObject indicatorObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bug" || collision.collider.tag == "Playfield")
        {
            this.gameObject.layer = LayerMask.NameToLayer("Playfield");
            this.gameObject.transform.parent.transform.parent = this.playfieldTransform;

            Scorekeeper.AddObject();

            this.indicatorObject.SetActive(false);

            this.grabbed = true;
        }
    }

    private void Update()
    {
        if (this.grabbed == true)
        {
            return;
        }
    
        Vector3 newPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (this.fallSpeed * Time.deltaTime), this.gameObject.transform.position.z);

        this.gameObject.transform.parent.position = newPosition;
    }
}
