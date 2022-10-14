using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquitoes : Entity
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Proyectil":
                livePoints -= collision.gameObject.GetComponentInParent<Plants>().mainAttack.DamageCalculator(gameObject.GetComponent<Plants>(), collision.gameObject.GetComponentInParent<Plants>());
                print(livePoints);
                break;
        }
    }
}
