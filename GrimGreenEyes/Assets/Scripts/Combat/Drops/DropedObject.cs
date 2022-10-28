using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedObject : MonoBehaviour
{
    public new string name;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player")
        {
            return;
        }
        PickUpObject();
        Destroy(gameObject);
    }
    private void PickUpObject()
    {

    }
}
