using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public void SetPlantType(Color newColor)
    {
        GetComponent<Image>().color = newColor;
    }
}
