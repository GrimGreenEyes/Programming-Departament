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
}
