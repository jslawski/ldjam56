using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BugPlacer : MonoBehaviour
{
    private float timeBetweenPlacements = 0.02f;

    private Coroutine placingCoroutine;

    [SerializeField]
    private GameObject bugPrefab;

    [SerializeField]
    private Transform bugParent;

    [SerializeField]
    private LayerMask collisionLayer;

    [SerializeField]
    private AudioClip bugSpawnSound;

    //private float deadZoneAngle = 1.0f;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (this.placingCoroutine == null)
            {
                this.placingCoroutine = StartCoroutine(this.PlaceBugCoroutine());
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator PlaceBugCoroutine()
    {        
        Ray newMouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray oldMouseRay = newMouseRay;
        RaycastHit oldHit = new RaycastHit();
        RaycastHit newHit;
        
        while (Input.GetMouseButton(0) == true && Physics.Raycast(newMouseRay, out newHit, float.PositiveInfinity, this.collisionLayer, QueryTriggerInteraction.Ignore))
        {
            float angle = Vector3.Angle(oldMouseRay.direction, newMouseRay.direction);

            this.SpawnBug(newHit.collider, newHit.point, newHit.normal);
            /*
            if (oldHit.collider != null && angle <= this.deadZoneAngle)
            {
                this.SpawnBug(oldHit.collider, oldHit.point, oldHit.normal);
            }
            else 
            {
                this.SpawnBug(newHit.collider, newHit.point, newHit.normal);
            }
            */
        
            yield return new WaitForSeconds(this.timeBetweenPlacements);

            oldMouseRay = newMouseRay;
            newMouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            oldHit = newHit;
        }

        this.placingCoroutine = null;
    }

    private void SpawnBug(Collider spawnCollider, Vector3 position, Vector3 normal)
    {
        GameObject newBug = Instantiate(this.bugPrefab, position, new Quaternion(), this.bugParent);

        newBug.transform.forward = -normal;

        float randomZRotation = Random.Range(0.0f, 360);

        Transform[] bugTransforms = newBug.GetComponentsInChildren<Transform>();
        
        bugTransforms[1].rotation = Quaternion.Euler(newBug.transform.rotation.eulerAngles.x, newBug.transform.rotation.eulerAngles.y, randomZRotation);
        /*
        if (spawnCollider.tag == "Bug")
        {
            BugBoy newBugBoy = newBug.GetComponentInChildren<BugBoy>();
            BugBoy oldBugBoy = spawnCollider.gameObject.GetComponentInChildren<BugBoy>();

            newBugBoy.prevBug = oldBugBoy;
            oldBugBoy.nextBug = newBugBoy;
        }
        */
        
        AudioChannelSettings channelSettings = new AudioChannelSettings(false, 0.8f, 1.2f, 0.1f, "SFX", newBug.transform);

        AudioManager.instance.Play(this.bugSpawnSound, channelSettings);
    }

    private void DestroyAllBugs()
    {
        Transform[] allBugs = this.bugParent.gameObject.GetComponentsInChildren<Transform>();

        for (int i = 1; i < allBugs.Length; i++)
        {
            Destroy(allBugs[i].gameObject);
        }
    }
}
