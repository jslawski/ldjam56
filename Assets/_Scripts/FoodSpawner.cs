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

    private float timeBetweenFoodSpawns = 5.0f;

    [SerializeField]
    private Transform playfieldParent;

    private void Start()
    {
        this.spawnSpace = GetComponent<BoxCollider>();
        this.foodPrefabs = Resources.LoadAll<GameObject>("FoodObjects");

        this.minFieldX = spawnSpace.transform.position.x - (spawnSpace.size.x / 2);
        this.maxFieldX = spawnSpace.transform.position.x + (spawnSpace.size.x / 2);
        this.minFieldZ = spawnSpace.transform.position.z - (spawnSpace.size.z / 2);
        this.maxFieldZ = spawnSpace.transform.position.z + (spawnSpace.size.z / 2);

        StartCoroutine(this.SpawnFoodCoroutine());
    }

    private IEnumerator SpawnFoodCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.timeBetweenFoodSpawns);

            this.SpawnFood();

        }
    }

    private void SpawnFood()
    {
        float xValue = Random.Range(this.minFieldX, this.maxFieldX);
        float zValue = Random.Range(this.minFieldZ, this.maxFieldZ);

        Vector3 worldSpaceGenerationPosition = new Vector3(xValue, this.transform.position.y, zValue);
        GameObject objectToGenerate = this.foodPrefabs[Random.Range(0, this.foodPrefabs.Length)];
        GameObject objectInstance = Instantiate(objectToGenerate, worldSpaceGenerationPosition, new Quaternion(), this.transform) as GameObject;

        GrabbableObject grabbableInstance = objectInstance.GetComponentInChildren<GrabbableObject>();

        grabbableInstance.playfieldTransform = this.playfieldParent;

        float randomXRotation = Random.Range(0.0f, 360);
        float randomYRotation = Random.Range(0.0f, 360);
        float randomZRotation = Random.Range(0.0f, 360);

        grabbableInstance.gameObject.transform.rotation = Quaternion.Euler(randomXRotation, randomYRotation, randomZRotation);
    }
}
