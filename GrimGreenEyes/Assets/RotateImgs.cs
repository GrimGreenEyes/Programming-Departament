using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImgs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(gameObject.transform.position, new Vector3(0,0,1), 20 *Time.deltaTime);
    }
}
