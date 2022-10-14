using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Info")]
    public new string name;

    [Header("Stats")]
    public int livePoints;
    public int maxLivePoints;
    public int attack;
    public int defense;
    public int heatResistance;
    public int freezeResistance;
    public int agility;
    public const int movement = 6;


    public int attackMultiplayer;
    public int defenseMultiplayer;
    public int heatResistanceMultiplayer;
    public int freezeResistanceMultiplayer;

    public bool bleeding = false;


    public Attack mainAttack;
}
