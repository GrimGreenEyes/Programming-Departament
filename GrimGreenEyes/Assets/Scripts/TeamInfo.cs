using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamInfo : MonoBehaviour
{
    //ELEMENTOS CLAVE PARA DEFINIR EL ESTADO DE LA PARTIDA
    public List<PlantInfo> plantsList = new List<PlantInfo>(); //LISTA DE TODAS LAS PLANTAS DEL EQUIPO
    public Dictionary<Item, int> itemsDictionary = new Dictionary<Item, int>(); //DICCIONARIO DE TODOS LOS ITEMS ALMACENADOS (item, cantidad)
    public Dictionary<Fertilizer, int> fertilizersDictionary = new Dictionary<Fertilizer, int>(); //DICCIONARIO DE TODOS LOS FERTILIZANTES ALMACENADOS(fertilizante, cantidad)
    public int waterAmount; //CANTIDAD DE AGUA ALMACENADA

    //Variables internas
    [SerializeField] private Item horn, wing, abdomen, chest, leg, shell, aloeVeraS, cactusS, carnivorousFungusS, cornS, lilyS, roseS, sunflowerS, tumbleweedS;

    //FUNCIONES PARA COSULTAR EL EQUIPO A ALTO NIVEL
    public List<PlantInfo> GetPlantsList() { return plantsList; }

    //FUNCIONES PARA AÑADIR OBJETOS AL INVENTARIO A ALTO NIVEL
    public void AddItemHorn() { AddItem(horn); }
    public void AddItemWing() { AddItem(wing); }
    public void AddItemAbdomen() { AddItem(abdomen); }
    public void AddItemChest() { AddItem(chest); }
    public void AddItemLeg() { AddItem(leg); }
    public void AddItemShell() { AddItem(shell); }
    public void AddSeedAloeVera() { AddItem(aloeVeraS); }
    public void AddSeedCactus() { AddItem(cactusS); }
    public void AddSeedCarnivorousFungus() { AddItem(carnivorousFungusS); }
    public void AddSeedCorn() { AddItem(cornS); }
    public void AddSeedLily() { AddItem(lilyS); }
    public void AddSeedRose() { AddItem(roseS); }
    public void AddSeedSunflower() { AddItem(sunflowerS); }
    public void AddSeedTumbleweed() { AddItem(tumbleweedS); }
    public void AddWater(int amountToAdd) { waterAmount += amountToAdd; }


    //Funciones internas
    private void AddItem(Item item)
    {
        if (itemsDictionary.ContainsKey(item))
        {
            itemsDictionary[item] = itemsDictionary[item] + 1;
        }
        else
        {
            itemsDictionary.Add(item, 1);
        }
    }
}

public class PlantInfo : MonoBehaviour
{
    //ELEMENTOS CLAVE PARA DEFINIR EL ESTADO DE LA PLANTA
    public GameObject plantTypePrefab; //TIPO BASE DE LA PLANTA (si se quiere saber el NOMBRE del tipo de planta, está almacenado en plantType.name)
    public Dictionary<StatRes, int> statsDictionary = new Dictionary<StatRes, int>(); //DICCIONARIO DE TODAS LAS ESTADÍSTICAS DE LA PLANTA (stat, valor)
    public List<GameObject> skillsList = new List<GameObject>(); //LISTA DE LAS HABILIDADES DE LA PLANTA
    public int healthPoints; //CANTIDAD DE VIDA ACTUAL (en el diccionario se almacena únicamente la vida máxima)

    //Variables internas
    public StatRes agility, attack, coldResistance, deffense, health, heatRessistance, movement;
    public List<SkillRes> skillsInternal = new List<SkillRes>();

    //FUNCIONES PARA MODIFICAR / CARGAR LA PLANTA A ALTO NIVEL
    public GameObject GetPlantType()
    {
        return plantTypePrefab;
    }

    public int GetPlantAttack() { return GetStat(attack); }
    public int GetPlantDeffense() { return GetStat(deffense); }
    public int GetPlantAgility() { return GetStat(agility); }
    public int GetPlantColdRessistance() { return GetStat(coldResistance); }
    public int GetPlantHeatRessistance() { return GetStat(heatRessistance); }
    public int GetPlantMovemente() { return GetStat(movement); }
    public int GetPlantHealth() { return GetStat(health); }
    
    public int GetCurrentHP()
    {
        return healthPoints;
    }

    public void SetCurrentHP(int newHP)
    {
        healthPoints = newHP;
    }

    public List<GameObject> GetPlantSkills()
    {
        return skillsList;
    }

    //Funciones internas
    private int GetStat(StatRes stat)
    {
        return statsDictionary[stat];
    }

    private void AddSkill(SkillRes skill)
    {
        skillsInternal.Add(skill);
        skillsList.Add(skill.prefab);
    }
}
