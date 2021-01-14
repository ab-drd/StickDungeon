using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corrupt : Unit
{
    public bool DoAttack()
    {
        var rand = new System.Random();

        if (rand.Next(11) < 7)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}