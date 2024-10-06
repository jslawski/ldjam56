using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugEraser : MonoBehaviour
{
    [SerializeField]
    private LayerMask collisionLayer;

    private float forceMagnitude = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            this.EraseBugs();
        }
    }

    private void EraseBugs()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.SphereCastAll(mouseRay, 0.01f, float.PositiveInfinity, this.collisionLayer, QueryTriggerInteraction.Ignore);

        if (hits != null)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                this.DetachBug(hits[i]);
            }
        }
    }

    private void DetachBug(RaycastHit hit)
    {
        if (hit.collider.tag != "Bug")
        {
            return;
        }

        BugBoy bugToDetach = hit.collider.gameObject.GetComponentInChildren<BugBoy>();
        bugToDetach.gameObject.transform.parent.transform.parent = null;
        Rigidbody bugRigidbody = bugToDetach.gameObject.AddComponent<Rigidbody>();

        Vector3 randomVector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

        bugRigidbody.AddForce(randomVector * this.forceMagnitude, ForceMode.Impulse);

        if (bugToDetach.prevBug != null)
        {
            bugToDetach.prevBug.nextBug = null;
        }
        if (bugToDetach.nextBug != null)
        {
            bugToDetach.nextBug.prevBug = null;
        }

        
    }
}
