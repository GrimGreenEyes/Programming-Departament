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
    [SerializeField] private StatRes agility, attack, coldResistance, deffense, health, heatRessistance, movement;

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

    public void StorePlantsList(Plant[] originalList)
    {
        plantsList = new List<PlantInfo>();

        foreach (Plant plant in originalList)
        {
            if (plant != null) {

            PlantInfo aux = new PlantInfo();

            aux.agility = agility;
            aux.attack = attack;
            aux.coldResistance = coldResistance;
            aux.deffense = deffense;
            aux.health = health;
            aux.heatRessistance = heatRessistance;
            aux.movement = movement;

            aux.plantTypePrefab = plant.plantType.prefab;
            aux.plantTypeInternal = plant.plantType;
            aux.statsDictionary = plant.statsDictionary;
            aux.skillsInternal = plant.skillsList;

            aux.healthPoints = plant.healthPoints;
            aux.plantState = plant.plantState;

            aux.Initialize();
            plantsList.Add(aux);
            }
        }

        //DEBUG
        /*Debug.Log("storing - Stored plants:");
        int i = 0;
        foreach(PlantInfo plant in plantsList)
        {
            Debug.Log("storing - Planta " + i.ToString() + ": " + plant.GetPlantType().name + " - MOV: " + plant.GetPlantMovement().ToString() + " - HP: " + plant.GetCurrentHP().ToString());
            i++;
        }*/
        //END
    }

    private void Start()
    {
        AddInitialPlant();
    }

    [SerializeField] private PlantType lilyPlantType;
    [SerializeField] private GameObject lilyPrefab, shieldPrefab;
    [SerializeField] private SkillRes shieldSkill;

    public void AddInitialPlant()
    {
        PlantInfo initialPlant = new PlantInfo();
        initialPlant.plantTypeInternal = lilyPlantType;
        initialPlant.plantTypePrefab = lilyPrefab;

        initialPlant.statsDictionary = new Dictionary<StatRes, int>();
        initialPlant.statsDictionary.Add(agility, lilyPlantType.baseAgility);
        initialPlant.statsDictionary.Add(attack, lilyPlantType.baseAttack);
        initialPlant.statsDictionary.Add(coldResistance, lilyPlantType.baseColdDef);
        initialPlant.statsDictionary.Add(deffense, lilyPlantType.baseDeffense);
        initialPlant.statsDictionary.Add(health, lilyPlantType.baseHP);
        initialPlant.statsDictionary.Add(heatRessistance, lilyPlantType.baseHeatDef);
        initialPlant.statsDictionary.Add(movement, lilyPlantType.baseMovement);

        initialPlant.skillsList = new List<GameObject>();
        initialPlant.skillsList.Add(shieldPrefab);

        initialPlant.healthPoints = lilyPlantType.baseHP;

        initialPlant.agility = agility;
        initialPlant.attack = attack;
        initialPlant.coldResistance = coldResistance;
        initialPlant.deffense = deffense;
        initialPlant.health = health;
        initialPlant.heatRessistance = heatRessistance;
        initialPlant.movement = movement;

        initialPlant.skillsInternal = new List<SkillRes>();
        initialPlant.skillsInternal.Add(shieldSkill);

        initialPlant.plantState = 2;

        plantsList.Add(initialPlant);
    }
}

public class PlantInfo
{
    //ELEMENTOS CLAVE PARA DEFINIR EL ESTADO DE LA PLANTA
    public GameObject plantTypePrefab; //TIPO BASE DE LA PLANTA (si se quiere saber el NOMBRE del tipo de planta, está almacenado en plantType.name)
    public Dictionary<StatRes, int> statsDictionary = new Dictionary<StatRes, int>(); //DICCIONARIO DE TODAS LAS ESTADÍSTICAS DE LA PLANTA (stat, valor)
    public List<GameObject> skillsList = new List<GameObject>(); //LISTA DE LAS HABILIDADES DE LA PLANTA
    public int healthPoints; //CANTIDAD DE VIDA ACTUAL (en el diccionario se almacena únicamente la vida máxima)

    //Variables internas
    public StatRes agility, attack, coldResistance, deffense, health, heatRessistance, movement;
    public List<SkillRes> skillsInternal = new List<SkillRes>();
    public PlantType plantTypeInternal;
    public int plantState;

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
    public int GetPlantMovement() { return GetStat(movement); }
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

    public void Initialize()
    {
        foreach (SkillRes skill in skillsInternal)
        {
            if (skill != null)
            {
            skillsList.Add(skill.prefab);
            }
        }
    }
}
