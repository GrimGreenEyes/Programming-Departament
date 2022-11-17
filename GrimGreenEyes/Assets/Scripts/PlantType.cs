using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlantType", order = 2)]
public class PlantType : ScriptableObject
{
    //Almacena las propiedades del tipo de planta
    public string name;
    public int baseHP;
    public Sprite[] spriteSheet;
    public int baseAttack, baseMovement, baseAgility, baseDeffense, baseHeatDef, baseColdDef;
    public SkillRes baseSkill;
    public GameObject prefab;

    public int upgradeAttack, upgradeDeffense, upgradeAgility, upgradeMovement, upgradeHeatDef, upgradeColdDef, upgradeHP;


    //TEMPORAL (DEBE SER OTRO SCRIPTABLEOBJECT PARA ALMACENAR DESCRIPCIÓN)
    public string attack;
}
