using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Bichous : Entity
{
    public GameController global;
    //  public TeamInfo globalInfo;
    public List<GameObject> plants;
    [SerializeField] public RelevantInfoPlantInsect[] plantsInfo;

    public Factor[] _distToPlantsLeaf;
    public Factor[] _distToPlantsLinear;

    public float _PesoDP;
    public float _PesoATT;

    public List<float> options;

    UtilitySystemEngine utilEngine;

    private void Update()
    {
         States();
        livePointsText.text = livePoints.ToString() + "/" + maxLivePoints.ToString();
    }
    //ESCARABAJO
    private void Start()
    {
        if (_PesoDP == 0)
            _PesoDP = 0.5f;
        if (_PesoATT == 0)
            _PesoATT = 0.5f;

        global = GameObject.Find("GameController").GetComponent<GameController>();
        //  globalInfo = GameObject.Find("GlobalAttributes").GetComponent<TeamInfo>();
        plants = GameController.instance.players;
        plantsInfo = new RelevantInfoPlantInsect[plants.Count];
        _distToPlantsLeaf = new Factor[plantsInfo.Length];
        _distToPlantsLinear = new Factor[plantsInfo.Length];

        Debug.Log(plants[0].GetComponent<Plants>().name);

        //INICIALIZACION DATOS
        for (int i = 0; i < plants.Count; i++)
        {
            plantsInfo[i] = new RelevantInfoPlantInsect();
            // Debug.Log(plants[i].GetComponent<Plants>().name);

            plantsInfo[i].Init(plants[i].GetComponent<Plants>(), gameObject.GetComponent<Bichous>());
            plantsInfo[i].UpdateRelevant();

            // Debug.Log(i + " la i::  " +  plantsInfo[i].GridX + "vs:: " + plants[i].GetComponent<Plants>().gridX);
        }
     

        utilEngine = new UtilitySystemEngine(false, 1.0f);



        //Factor vidaCarro = new LeafVariable(() => vidaCarroActual, vidaCarroMax, vidaCarroMin);
        // Igual pero con distancias
        //

        //  List<Factor> factorsDistP = new List<Factor>();
        //List<float> weightsDisP = new List<float>();

        /* for (int i = 0; i < plantsInfo.Length; i++)
         {
             //_distToPlantsLeaf[i] = new Factor();
             Factor _distToPlantsLeafHelper = new LeafVariable(() => plantsInfo[i].distToPlant, 33, 1);
             _distToPlantsLeaf[i] = _distToPlantsLeaf[i];
             factorsDistP.Add(_distToPlantsLeaf[i]);
             weightsDisP.Add(_PesoDP);
         }
        */

        //   Factor weightFactor = new WeightedSumFusion(factorsDistP, weightsDisP);



        // Factores
        /*  List<Factor> factors = new List<Factor>();
          Factor _distToPlantsLeafHelper1 = new LeafVariable(() => plantsInfo[0].distToPlant, 33, 1);
          Factor _distToPlantsLeafHelper2 = new LeafVariable(() => plantsInfo[1].distToPlant, 33, 1);

          factors.Add(_distToPlantsLeafHelper1);

          factors.Add(_distToPlantsLeafHelper2);
          // Pesos
          List<float> weights = new List<float>();
          weights.Add(0.4f);
          weights.Add(0.6f);
          // Weighted Sum 2 opciones
          Factor weightFactor = new WeightedSumFusion(factors, weights);
          utilEngine.CreateUtilityAction("ataque", attack, weightFactor);
        */
        // Debug.Log(weightFactor.getValue());
        States();
        
    }


    public override void States()
    {
        switch (actualState)
        {
            case EntityState.START:
                movement = maxMovement;
                attacked = false;
                if (bleeding) livePoints -= 5;
                if (livePoints <= 0)
                {
                    actualState = EntityState.IDLE;
                    GameController.instance.NextPlayer();
                    GameController.instance.Died(gameObject);
                }
                for (int i = 0; i < skills.Count; i++)
                {
                    skills[i].ReduceCoolDown();
                }
                if (gameObject == GameController.instance.SelectedPlayer())
                {

                    PlayerPanel.instance.ChangePlayer(gameObject);
                }
                if (stuned)
                {
                    actualState = EntityState.FINISHED;
                }
                else
                {
                    actualState = EntityState.IDLE;
                }
                break;

            case EntityState.IDLE:
                if (gameObject != GameController.instance.SelectedPlayer())
                {
                    return;
                }
                Debug.Log("TURNOOO");

                PickOption();
                break;
            case EntityState.MOVEING:
                if (path == null)
                {
                    path = PathFinding.instance.PathFind(thisTile, destination);
                    pathPosition = path.Count() - 1;
                }
                Move();
                break;
            case EntityState.ATTACKING:
                Debug.Log("attacking");
                attacked = true;
                for (int i = 0; i < mainObjective.GetComponent<Entity>().skills.Count; i++)
                {
                    if (mainObjective.GetComponent<Entity>().skills[i].isReciveingDamage)
                        mainObjective.GetComponent<Entity>().skills[i].Effect(gameObject, mainObjective);
                }
                mainAttack.Effect(mainObjective, gameObject);
                break;
            case EntityState.USINGSKILL:
                //GridCreator.instance.ShineTiles(gridX, gridY, skills[skillSelected].radious, false);
                if (skills[skillSelected].actilveOnClick)
                {
                    skills[skillSelected].Effect(gameObject, GameController.instance.SelectedPlayer());
                }
                else
                {
                    GridCreator.instance.SearchObjective(gridX, gridY, skills[skillSelected].range, false);
                }
                break;
            case EntityState.FINISHED:
                GameController.instance.NextPlayer();
                break;
        
        }
        





        // }
    }

    public void PickOption()
    {
        //Attack Carro
        options.Add(0);
        //Imp Mov Carro
        options.Add(0);

        //Huir Planta
        options.Add(0.05f);
        //Atacar Planta X

        float[] plantsAtt = new float[plants.Count];
        //  float esRent = CalcRentable(plantsInfo[i]);
        for (int i = 0; i < plants.Count; i++)
        {
            List<Factor> factors = new List<Factor>();
            Factor _distToPlantsLeafHelper1 = new LeafVariable(() => plantsInfo[i].distToPlant, 1, 33);
            //  Factor _distToPlantsLeafHelper2 = new LeafVariable(() => plantsInfo[1].distToPlant, 33, 1);

            Factor esRent = new LeafVariable(() => CalcRentable(plantsInfo[i]), 33, 1);

            factors.Add(_distToPlantsLeafHelper1);

            factors.Add(esRent);
            // Pesos
            List<float> weights = new List<float>();
            weights.Add(0.4f);
            weights.Add(0.6f);
            // Weighted Sum 2 opciones
            Factor weightFactor = new WeightedSumFusion(factors, weights);
            plantsAtt[i] = weightFactor.getValue();

            Debug.Log(plantsAtt[i] + " " + plantsInfo[i].name);
        }
        int index = Array.IndexOf(plantsAtt, plantsAtt.Max());

        options.Add(plantsAtt[index]);

        Debug.Log(plantsAtt[index]);

        float maxValue = options.Max();
        int selection = options.ToList().IndexOf(maxValue, index);

        //GameController.instance.SelectedPlayer().GetComponent<Plants>().mainObjective = gameObject;
        //GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState = Entity.EntityState.ATTACKING;

        ExecuteSelected(selection, index);
        GameController.instance.NextPlayer();
        return;
   
    }

    public void ExecuteSelected(int decision, int indPlanta)
    {
        if(decision == 0)
        {
            //Atack Carro
            return;

        }
        else if(decision == 1)
        {
            //Impedir Mov Carro

        }
        else if(decision == 2)
        {
            //Huir de planta
        }
        //Si no es ninguno de estos sera atacar a una planta
        else if(decision == 3)
        {
            while(gameObject.GetComponent<Bichous>().movement != 0 && !attacked )
            if (plantsInfo[indPlanta].distToPlant <= 1) // 1 o range
            {
                    //Attack
                    attacked = true;
            }
                else
                {
                    MoveToClosePosition(plantsInfo[indPlanta]);
                    attacked = true;
                }

        }


    }

    public void MoveToClosePosition(RelevantInfoPlantInsect plantaObj)
    {
        //TO DO
        int destX = 0;
        int destY = 0;

        int xNeeded = gridX - (int)plantaObj.distX;
        int yNeeded = gridY - (int)plantaObj.distY;

        if (xNeeded <= movement)
        {
            destX = xNeeded;
            movement -= xNeeded;
        }
        else if (yNeeded <= movement)
        {
            destX = xNeeded;
            movement -= xNeeded;
        }
        else
        {
            destX = xNeeded - movement;
            
        }
           


        SetDestination(GridCreator.instance.GetTile((gridX + destX < 0 || gridX + destX > GridCreator.instance.width) ? gridX - destX : gridX + destX, (gridY + destY < 0 || gridY + destY > GridCreator.instance.height) ? gridY - destY : gridY + destY));

    }

    public int distPlantH(int dxy, int gridXY)
    {
        int distX = Mathf.Abs(gridXY - dxy);
        return distX;
    }




    public float CalcRentable(RelevantInfoPlantInsect planta)
    {
        return 1;
    }



        public float normalize(float rawValue, float min, float max)
    {
        float scaledValue = (rawValue - min) / (max - min);
        return scaledValue;
    }

    public void attack()
    {
        Debug.Log("EJECUTA ATTAQUE");
    }


    private void OnMouseDown()
    {
        if (UIHoverListener.instance.isUIOverride)
        {
            return;
        }
        if (GameController.instance.SelectedPlayer().tag != "Player")
        {
            return;
        }
        if (GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState == Entity.EntityState.USINGSKILL)
        {
            Debug.Log("skill");
            if (GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].isbuffing)
            {
                return;
            }
            if (Mathf.Abs(gridX - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridX) + Mathf.Abs(gridY - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridY) > GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].range)
            {
                return;
            }
            if (GameController.instance.SelectedPlayer().GetComponent<Entity>().skills[GameController.instance.SelectedPlayer().GetComponent<Entity>().skillSelected].alreadyUsed)
            {
                return;
            }
            Debug.Log("effect");

            GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].Effect(gameObject, GameController.instance.SelectedPlayer());
        }
        if (GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState == EntityState.IDLE)
        {
            if (Mathf.Abs(gridX - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridX) + Mathf.Abs(gridY - GameController.instance.SelectedPlayer().GetComponent<Plants>().gridY) > GameController.instance.SelectedPlayer().GetComponent<Plants>().mainAttack.range)
            {
                return;
            }
            if (GameController.instance.SelectedPlayer().GetComponent<Plants>().attacked)
            {
                return;
            }
            if (GameController.instance.SelectedPlayer().GetComponent<Plants>().mainAttack.directedToAlly)
            {
                return;
            }
            Debug.Log("main obgective");
            GameController.instance.SelectedPlayer().GetComponent<Plants>().mainObjective = gameObject;
            GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState = Entity.EntityState.ATTACKING;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag != "Feet")
        {
            return;
        }
        switch (collision.tag)
        {
            case "Proyectil":
                livePoints -= collision.gameObject.GetComponentInParent<Plants>().mainAttack.DamageCalculator(gameObject.GetComponent<Mosquitoes>(), collision.gameObject.GetComponentInParent<Plants>());
                print(livePoints);
                break;
        }
    }

    
}

[System.Serializable]
public class RelevantInfoPlantInsect : MonoBehaviour
{

    public string name;
    public int saludCarro;
    public int distCarroToEnd;
    public int distCarroToInsect;

    public int distToPlant;
    public float distX;
    public float distY;

    public int GridX;
    public int GridY;

    public Bichous thisInsect;

    public void Init(Plants plant, Bichous Insect)
    {
        name = plant.name;
        Debug.Log("INIT!!");
        GridX = plant.gridX;
        GridY = plant.gridY;
        thisInsect = Insect;
    }

    public void UpdateRelevant()
    {
        //getRealInfo
        distPlant();
    }

    public void distPlant()
    {
        distX = Mathf.Abs(thisInsect.gridX - GridX);
        distY = Mathf.Abs(thisInsect.gridY - GridY);

        float distTo = (distX + distY);
        distToPlant = ((int)distTo);
        Debug.Log(distToPlant + " " + name);
    }
}
