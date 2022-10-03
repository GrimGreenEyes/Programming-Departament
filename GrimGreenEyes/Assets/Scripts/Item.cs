using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    //Almacena las propiedades del objeto
    public string name;
    public Sprite sprite;
    public int type = Items.NULL;
    public PlantType seedType;
}

public static class Items
{
    public const int NULL = -1;
    public const int SEED = 1;
    public const int INSECT_BODY = 2;
    public const int FERTILIZER = 3;
}
