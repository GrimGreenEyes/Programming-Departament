using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    [SerializeField] private new TextMeshProUGUI name;

    public void SetName(string newName)
    {
        name.text = newName;
    }
}
