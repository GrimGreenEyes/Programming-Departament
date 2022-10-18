using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCorn : MonoBehaviour
{
    const float timeDestroy = 5f;
    private void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    public void throwTowards(Vector3 enemyPosition)
    {
        GetComponent<Rigidbody2D>().velocity = enemyPosition - transform.position; 
    }
}
