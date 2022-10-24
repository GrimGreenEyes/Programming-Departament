using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StatRes", order = 4)]
public class StatRes : ScriptableObject
{
    public string name;
    [TextArea(3, 10)]
    public string description;
}
