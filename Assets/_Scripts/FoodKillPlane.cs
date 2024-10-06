using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodKillPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sandwich")
        {
            this.gameOverPanel.SetActive(true);
        }

        Destroy(other.gameObject);
    }
}