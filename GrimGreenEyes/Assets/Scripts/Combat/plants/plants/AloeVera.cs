using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AloeVera : Plants
{
    private MagicSap a;
    private Skill s;
    private void Awake()
    {

        SetAttack(a);
        SetSkill(s);
    }
}
