using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBoy : MonoBehaviour
{
    public BugBoy nextBug = null;
    public BugBoy prevBug = null;

    public void KillBug()
    {
        Scorekeeper.RemoveBug();
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
