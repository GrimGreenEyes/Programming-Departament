using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public new string name;
    public string description;
    public bool isActiveSkill;
    public bool isbuffing;
    public int radious;


    public virtual void Effect() { }
}
