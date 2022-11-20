using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDebugger : MonoBehaviour
{
    public GameObject debugger;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            debugger.SetActive(!debugger.active);
        }
    }
}
