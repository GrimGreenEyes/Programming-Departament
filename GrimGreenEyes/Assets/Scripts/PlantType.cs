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

    public int idleIndex;
    public string attack;
    [TextArea(3, 10)]
    public string actionDescription;
}
