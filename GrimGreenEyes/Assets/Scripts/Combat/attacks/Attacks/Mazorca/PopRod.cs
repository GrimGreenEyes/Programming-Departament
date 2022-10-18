using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopRod : Attack
{
    [SerializeField] private GameObject popCornPref;
    private GameObject popCorn;

    public override void Effect(GameObject enemy, GameObject player)
    {
        Debug.Log("lanzar palomita");
        popCorn = Instantiate(popCornPref, player.transform);
        popCorn.GetComponent<PopCorn>().throwTowards(enemy.transform.position);
    }
}
