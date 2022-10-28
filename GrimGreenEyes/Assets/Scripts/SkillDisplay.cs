using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillDisplay : MonoBehaviour
{
    public TextMeshProUGUI skillText;

    public void DisplaySkill(string text)
    {
        skillText.text = text;
    }
}
