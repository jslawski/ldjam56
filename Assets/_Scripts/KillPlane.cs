using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private Queue<BugBoy> killBugQueue;

    private int batchSize = 1;

    private void Start()
    {
        this.killBugQueue = new Queue<BugBoy>();

        StartCoroutine(this.ProcessKilledBugs());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bug")
        {
            this.killBugQueue.Enqueue(other.GetComponent<BugBoy>());
        }
    }

    private IEnumerator ProcessKilledBugs()
    {
        int currentBatchSize = 0;
    
        while (true)
        {
            if (this.killBugQueue.Count > 0)
            {
                if (currentBatchSize > this.batchSize)
                {
                    for (int i = 0; i < this.batchSize; i++)
                    {
                        BugBoy killedBug = this.killBugQueue.Dequeue();
                        killedBug.KillBug();
                    }

                    currentBatchSize = 0;
                }
                else
                {
                    currentBatchSize++;
                }
            }

            yield return null;
        }
    }
}
