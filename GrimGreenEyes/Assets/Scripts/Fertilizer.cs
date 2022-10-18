using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Fertilizer", order = 2)]
public class Fertilizer : ScriptableObject
{
    //Almacena las propiedades del objeto
    public string name;
    public Sprite sprite;
    public int type = 1; //0 for LVLUP fertilizer; 1 for SPECIAL fertilizer
    public Skill skill;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skill", order = 3)]
public class Skill : ScriptableObject
{
    //Almacena las propiedades del objeto
    public string name;
    public string description;
    public Color color;
}
