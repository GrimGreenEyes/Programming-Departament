using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlantType", order = 2)]
public class PlantType : ScriptableObject
{
    //Almacena las propiedades del tipo de planta
    public string name;
    //public Color color;
    public int baseHP;
    public Sprite[] spriteSheet;
    public int baseDamage, baseMovement, baseAgility, baseInitiative;
}
