using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueHolder
{
    public Queue<GameObject> goQueue = new Queue<GameObject>();

    public bool AddToQueue(GameObject go)
    {
        goQueue.Enqueue(go);

        if (goQueue.Count > 3)
        {
            return true;
        }

        return false;
    }
}
