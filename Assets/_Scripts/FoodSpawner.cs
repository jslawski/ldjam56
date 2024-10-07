using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private BoxCollider spawnSpace;

    private float minFieldX;
    private float maxFieldX;
    private float minFieldZ;
    private float maxFieldZ;

    private GameObject[] foodPrefabs;

    private float maxTimeBetweenFoodSpawns = 5.0f;

    [SerializeField]
    private Transform playfieldParent;

    [SerializeField]
    private AnimationCurve expandCurve;

    private float currentExpansionTime = 0.0f;
    private float totalExpansionTimeInSeconds = 120;  //2 min

    private float minSize = 2.0f;
    private float maxSize = 4.0f;

    private Vector3 lastSpawnLocation = Vector3.zero;

    private float minXDistanceFromOtherObject = 0.9f;
    private float minZDistanceFromOtherObject = 0.9f;

    private int maxNumberRetries = 20;

    private void Start()
    {
        this.spawnSpace = GetComponent<BoxCollider>();
        this.foodPrefabs = Resources.LoadAll<GameObject>("FoodObjects");

        this.minFieldX = spawnSpace.transform.position.x - (spawnSpace.size.x / 2);
        this.maxFieldX = spawnSpace.transform.position.x + (spawnSpace.size.x / 2);
        this.minFieldZ = spawnSpace.transform.position.z - (spawnSpace.size.z / 2);
        this.maxFieldZ = spawnSpace.transform.position.z + (spawnSpace.size.z / 2);

        StartCoroutine(this.SpawnFoodCoroutine());
        StartCoroutine(this.ExpandFoodSpawner());
    }

    private IEnumerator SpawnFoodCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.maxTimeBetweenFoodSpawns);
            this.SpawnFood();
        }
    }

    private IEnumerator ExpandFoodSpawner()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            this.currentExpansionTime += Time.fixedDeltaTime;

            float curveValue = this.expandCurve.Evaluate(this.currentExpansionTime / this.totalExpansionTimeInSeconds);

            float newSize = Mathf.Lerp(this.minSize, this.maxSize, curveValue);

            this.spawnSpace.size = new Vector3(newSize, this.spawnSpace.size.y, newSize);

            this.minFieldX = spawnSpace.transform.position.x - (spawnSpace.size.x / 2);
            this.maxFieldX = spawnSpace.transform.position.x + (spawnSpace.size.x / 2);
            this.minFieldZ = spawnSpace.transform.position.z - (spawnSpace.size.z / 2);
            this.maxFieldZ = spawnSpace.transform.position.z + (spawnSpace.size.z / 2);
        }
    }

    private void SpawnFood()
    {
        Vector3 chosenSpawnPoint = this.GetRandomSpawnPoint();   
    
        GameObject objectToGenerate = this.foodPrefabs[Random.Range(0, this.foodPrefabs.Length)];
        GameObject objectInstance = Instantiate(objectToGenerate, chosenSpawnPoint, new Quaternion(), this.transform) as GameObject;

        GrabbableObject grabbableInstance = objectInstance.GetComponentInChildren<GrabbableObject>();

        grabbableInstance.playfieldTransform = this.playfieldParent;

        float randomXRotation = Random.Range(0.0f, 360);
        float randomYRotation = Random.Range(0.0f, 360);
        float randomZRotation = Random.Range(0.0f, 360);

        grabbableInstance.gameObject.transform.rotation = Quaternion.Euler(randomXRotation, randomYRotation, randomZRotation);

        this.lastSpawnLocation = chosenSpawnPoint;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 returnVector = this.gameObject.transform.position;
    
        for (int i = 0; i < this.maxNumberRetries; i++)
        {
            float xValue = Random.Range(this.minFieldX, this.maxFieldX);
            float zValue = Random.Range(this.minFieldZ, this.maxFieldZ);

            if (Mathf.Abs(this.lastSpawnLocation.x - xValue) <= this.minXDistanceFromOtherObject &&
                    Mathf.Abs(this.lastSpawnLocation.z - zValue) <= this.minZDistanceFromOtherObject)
            {
                xValue = Random.Range(this.minFieldX, this.maxFieldX);
                zValue = Random.Range(this.minFieldZ, this.maxFieldZ);
            }
            else
            {
                returnVector = new Vector3(xValue, this.transform.position.y, zValue);                

                break;
            }
        }

        return returnVector;
    }
}
