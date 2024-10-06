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
        RaycastHit hitInfo;
        if (Physics.SphereCast(mouseRay, 0.01f, out hitInfo))
        {
            StartCoroutine(this.DetachBugBranch(hitInfo.collider.gameObject.GetComponentInChildren<BugBoy>()));
        }

        /*
        if (hits != null)
        {
            hits[i]
        
            /*for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == "Bug")
                {
                    StartCoroutine(this.DetachBugBranch(hits[i].collider.gameObject.GetComponentInChildren<BugBoy>()));
                }
            }
        }
        */
        
    }

    private IEnumerator DetachBugBranch(BugBoy bugToDetach)
    {
        if (bugToDetach != null)
        {
            bugToDetach.gameObject.transform.parent.transform.parent = null;
            Rigidbody bugRigidbody = bugToDetach.gameObject.AddComponent<Rigidbody>();

            Vector3 randomVector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

            bugRigidbody.AddForce(randomVector * this.forceMagnitude, ForceMode.Impulse);

            yield return new WaitForFixedUpdate();

            if (bugToDetach.prevBug != null)
            {
                bugToDetach.prevBug.nextBug = null;
            }
            if (bugToDetach.nextBug != null)
            {
                bugToDetach.nextBug.prevBug = null;
                StartCoroutine(this.DetachBugBranch(bugToDetach.nextBug));
            }
        }
    }
}
