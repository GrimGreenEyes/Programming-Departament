using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plants : MonoBehaviour
{
    public new string name;

    public int livePoints;
    public float attack;
    public float defense;
    public float speed;
    public float agility;
    public float heatResistance;
    public float coldResistance;

    private Attack mainAttack;
    private Skill mainSkill;
}
