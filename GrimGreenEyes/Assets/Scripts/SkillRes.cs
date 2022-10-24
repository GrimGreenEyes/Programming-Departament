using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SkillRes", order = 3)]
public class SkillRes : ScriptableObject
{
    //Almacena las propiedades del objeto
    public string name;
    [TextArea(3, 10)]
    public string description;
    //public Color color;
}
