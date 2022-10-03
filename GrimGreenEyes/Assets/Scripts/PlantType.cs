using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlantType", order = 2)]
public class PlantType : ScriptableObject
{
    //Almacena las propiedades del tipo de planta
    public string name;
    public Sprite sprite;
    public Color color;
}
