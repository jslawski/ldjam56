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
    [SerializeField]
    private GameObject sparkleObject;
    [SerializeField]
    private ParticleSystem obtainParticles;
    [SerializeField]
    private MeshRenderer foodRenderer;
    [SerializeField]
    private Material nonFresnelMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bug" || other.tag == "Playfield" || other.tag == "Sandwich")
        {
            this.gameObject.layer = LayerMask.NameToLayer("Playfield");
            this.gameObject.tag = "Playfield";
            this.gameObject.GetComponent<Collider>().isTrigger = false;
            this.gameObject.transform.parent.transform.parent = this.playfieldTransform;

            Scorekeeper.AddObject();

            this.indicatorObject.SetActive(false);
            this.sparkleObject.SetActive(false);
            this.obtainParticles.Play();
            this.foodRenderer.material = this.nonFresnelMaterial;

            this.grabbed = true;
        }
    }

    private void FixedUpdate()
    {
        if (this.grabbed == true)
        {
            return;
        }
    
        this.gameObject.transform.parent.Translate(Vector3.down * this.fallSpeed * Time.fixedDeltaTime);
    }
}
