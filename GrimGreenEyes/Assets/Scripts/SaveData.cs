using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private PotionData _PotionData = new PotionData();


    [SerializeField] private GlobalVar _GlobalVar;


    public void SaveIntoJson()
    {
        _GlobalVar = GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>();
        string globalData = JsonUtility.ToJson(_GlobalVar);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PotionData.json", globalData);

        Debug.Log(Application.persistentDataPath);
    }


    public void getJSON()
    {
        JsonUtility.FromJson<GlobalVar>("PotionData.json");

    }
}

[System.Serializable]
public class PotionData
{
    public string potion_name;
    public int value;
    public List<Effect> effect = new List<Effect>();
}

[System.Serializable]
public class Effect
{
    public string name;
    public string desc;
}

