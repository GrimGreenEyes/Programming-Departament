using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Attack
{
    public override void Effect(GameObject objective, GameObject player)
    {
        Debug.Log("Persecución");
    }
}
