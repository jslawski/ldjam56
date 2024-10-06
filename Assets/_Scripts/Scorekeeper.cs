using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Scorekeeper
{
    private static int bugCount = 0;
    private static int objectCount = 0;


    public static void AddBug()
    {
        Scorekeeper.bugCount++;
    }

    public static void RemoveBug()
    {
        Scorekeeper.bugCount--;
    }

    public static void AddObject()
    {
        Scorekeeper.objectCount++;
    }

    public static int GetBugCount()
    {
        return Scorekeeper.bugCount;
    }

    public static int GetObjectCount()
    {
        return Scorekeeper.objectCount;
    }
}
