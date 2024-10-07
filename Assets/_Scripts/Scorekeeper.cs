using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Scorekeeper
{
    private static int bugCount = 0;
    private static int objectCount = 0;
    private static int highestBugCount = 0;

    public static int objectPointValue = 300;
    public static int bugPointValue = 1;

    public static void Reset()
    {
        Scorekeeper.bugCount = 0;
        Scorekeeper.objectCount = 0;
        Scorekeeper.highestBugCount = 0;
    }

    public static void AddBug()
    {
        Scorekeeper.bugCount++;

        Scorekeeper.highestBugCount = Scorekeeper.bugCount;
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

    public static int GetHighestBugCount()
    {
        return Scorekeeper.highestBugCount;
    }

    public static int GetObjectCount()
    {
        return Scorekeeper.objectCount;
    }

    public static int GetObjectScore()
    {
        return (Scorekeeper.objectCount * Scorekeeper.objectPointValue);
    }

    public static int GetBugScore()
    {
        return (Scorekeeper.highestBugCount * Scorekeeper.bugPointValue);
    }

    public static int GetTotalScore()
    { 
        return (Scorekeeper.GetObjectScore() + Scorekeeper.GetBugScore());
    }
}
