using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Fertilizer", order = 2)]
public class Fertilizer : ScriptableObject
{
    //Almacena las propiedades del objeto
    public string name;
    public Sprite sprite;
    public int type = 1; //0 for STAT fertilizer; 1 for SKILL fertilizer
    public SkillRes skill;
    public StatRes stat;
}
