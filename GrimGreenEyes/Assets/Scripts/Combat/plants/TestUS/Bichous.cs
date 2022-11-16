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

    // public List<float> options;

    [SerializeField]public float[] options;
    UtilitySystemEngine utilEngine;

    public bool optionPicked = false;

    public GameObject enemyPicked;
    int indexPicked;

    
    int movementH;


    private void Update()
    {
         States();
        livePointsText.text = livePoints.ToString() + "/" + maxLivePoints.ToString();
    }
    //ESCARABAJO
    private void Start()
    {
        isWalking = false;
        int movementH = maxMovement;
        options = new float[4]; 

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
                movementH = maxMovement;

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
                optionPicked = false;
                break;

            case EntityState.IDLE:
                if (gameObject != GameController.instance.SelectedPlayer())
                {
                    return;
                }
                Debug.Log("TURNOOO");
                if (!optionPicked)
                {
                    PickOption();
                }
                
                else if (actualState != EntityState.MOVEING && !attacked && plantsInfo[indexPicked].distToPlant <= 2)
                {
                    GameController.instance.SelectedPlayer().GetComponent<Bichous>().mainObjective = GridCreator.instance.GetTile(enemyPicked.GetComponent<Entity>().gridX, enemyPicked.GetComponent<Entity>().gridY).GetComponent<Tile>().entity;

                    GameController.instance.SelectedPlayer().GetComponent<Mosquitoes>().actualState = Entity.EntityState.ATTACKING;

                    Debug.Log("attacking");
                    attacked = true;
                    for (int i = 0; i < mainObjective.GetComponent<Entity>().skills.Count; i++)
                    {
                        if (mainObjective.GetComponent<Entity>().skills[i].isReciveingDamage)
                            mainObjective.GetComponent<Entity>().skills[i].Effect(gameObject, mainObjective);
                    }
                    mainAttack.Effect(mainObjective, gameObject);
                    Debug.Log("ATTACK!!");
                    //optionPicked = true;
                }

                break;
            case EntityState.MOVEING:
                if (path == null || path.Count == 0)
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
                optionPicked = false;

                break;
        
        }
        





        // }
    }

    public void PickOption()
    {
        //Attack Carro
        //options.Add(0);
        options[0] = 0;
        //Imp Mov Carro
      //  options.Add(0);
        options[1] = 0;
        //Huir Planta
        //  options.Add(0.05f);
        options[2] = 0.05f;
        //Atacar Planta X

        float[] plantsAtt = new float[plants.Count];
        //  float esRent = CalcRentable(plantsInfo[i]);
        for (int i = 0; i < plants.Count; i++)
        {
            List<Factor> factors = new List<Factor>();
            plantsInfo[i].Init(plants[i].GetComponent<Plants>(), gameObject.GetComponent<Bichous>());
            //plantsInfo[i].UpdateRelevant();

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

       // options.Add(plantsAtt[index]);
        options[3]= plantsAtt[index];

        Debug.Log(plantsAtt[index]);

        float maxValue = options.Max();
        int selection = options.ToList().IndexOf(maxValue);

        //GameController.instance.SelectedPlayer().GetComponent<Plants>().mainObjective = gameObject;
        //GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState = Entity.EntityState.ATTACKING;

        ExecuteSelected(selection, index);
        GameController.instance.NextPlayer();
        optionPicked = true;
        return;
   
    }

    public void ExecuteSelected(int decision, int indPlanta)
    {
        enemyPicked = plants[indPlanta];
        indexPicked = indPlanta;

        if (decision == 0)
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
            
            if(gameObject.GetComponent<Bichous>().movementH != 0 && !attacked) 
            {
                plantsInfo[indPlanta].Init(plants[indPlanta].GetComponent<Plants>(), gameObject.GetComponent<Bichous>());
                if (plantsInfo[indPlanta].distToPlant <= 2) // 1 o range
                {
                    //Attack
                    GameController.instance.SelectedPlayer().GetComponent<Bichous>().mainObjective = GridCreator.instance.GetTile(enemyPicked.GetComponent<Entity>().gridX, enemyPicked.GetComponent<Entity>().gridY).GetComponent<Tile>().entity;
                    GameController.instance.SelectedPlayer().GetComponent<Mosquitoes>().actualState = Entity.EntityState.ATTACKING;

                    // attacked = true;
                    Debug.Log("ATTACK 2");
                    Debug.Log("attacking");
                    attacked = true;
                    for (int i = 0; i < mainObjective.GetComponent<Entity>().skills.Count; i++)
                    {
                        if (mainObjective.GetComponent<Entity>().skills[i].isReciveingDamage)
                            mainObjective.GetComponent<Entity>().skills[i].Effect(gameObject, mainObjective);
                    }
                    mainAttack.Effect(mainObjective, gameObject);

                }
                else
                {
                    (int newX,int newY) = MoveToClosePosition(plantsInfo[indPlanta]);
                    //attacked = true;

                    // plantsInfo[indPlanta].Init(plants[indPlanta].GetComponent<Plants>(), gameObject.GetComponent<Bichous>());
                    plantsInfo[indPlanta].GridX = newX;
                    plantsInfo[indPlanta].GridY = newY;
                    plantsInfo[indPlanta].UpdateRelevant();
                    
                }
                if (plantsInfo[indPlanta].distToPlant <= 2) // 1 o range
                {
                    //Attack
                    GameController.instance.SelectedPlayer().GetComponent<Bichous>().mainObjective = GridCreator.instance.GetTile(enemyPicked.GetComponent<Entity>().gridX, enemyPicked.GetComponent<Entity>().gridY).GetComponent<Tile>().entity;
                    //  attacked = true;
                    //GameController.instance.SelectedPlayer().GetComponent<Mosquitoes>().actualState = Entity.EntityState.ATTACKING;
                    GameController.instance.SelectedPlayer().GetComponent<Mosquitoes>().actualState = Entity.EntityState.MOVEING;
                    /* Debug.Log("ATTACK 3");
                     Debug.Log("attacking");
                     attacked = true;
                     StartCoroutine(waitToMove());
                     for (int i = 0; i < mainObjective.GetComponent<Entity>().skills.Count; i++)
                     {
                         if (mainObjective.GetComponent<Entity>().skills[i].isReciveingDamage)
                             mainObjective.GetComponent<Entity>().skills[i].Effect(gameObject, mainObjective);
                     }
                     mainAttack.Effect(mainObjective, gameObject);
                    */
                    States();
                }
            }

        }




    }

    public (int,int) MoveToClosePosition(RelevantInfoPlantInsect plantaObj)
    {
        //TO DO
        int destX = 0;
        int destY = 0;

        //int xNeeded = gridX - (int)plantaObj.distX;
        // yNeeded = gridY - (int)plantaObj.distY;

        int movedX = 0;
        int movedY = 0;

        bool contX = false;
        bool contY = false;

        movementH = maxMovement;

        movedX = gridX + destX;
        movedY = gridY + destY;

        while (movementH > 0 && (contX && contY) == false)
        {
            if (Mathf.Abs(movedX - plantaObj.GridX) < 2)
                contX = true;
            if (!contX)
            {
                //destX =  -Mathf.Clamp(Mathf.RoundToInt((int)plantaObj.GridX), -1, 1);
                if (plantaObj.GridX > movedX)
                    destX = 1;
                else
                    destX = -1;
                movedX += destX;
            movementH -= 1;
           // movement -= 1;


            }
            if (Mathf.Abs(movedY - plantaObj.GridY) < 2)
                contY = true;
            if (!contY)
            {
                //destY = -Mathf.Clamp(Mathf.RoundToInt((int)plantaObj.GridY), -1, 1);
                if (plantaObj.GridY > movedY)
                    destY = 1;
                else
                    destY = -1;
                movedY += destY;
                movementH -= 1;
               // movement -= 1;
            }
        }
        Debug.Log(movedX + ", " + movedY);
        Debug.Log(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY));
/*
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
           */


        SetDestination(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY));
        MovementPoint = transform.position;
        moveing = false;
        path = null;
        return (movedX, movedY);

    }

    public Tuple<int, int> GetMultipleValue()
    {
        return Tuple.Create(1, 2);
    }

    public int distPlantH(int dxy, int gridXY)
    {
        int distX = Mathf.Abs(gridXY - dxy);
        return distX;
    }

    System.Collections.IEnumerator waitToMove()
    {
        if (path == null)
        {
            path = PathFinding.instance.PathFind(thisTile, destination);
            pathPosition = path.Count() - 1;
        }
        

        while (isWalking)
        {
            Move();
        }
         yield return null;

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

        UpdateRelevant();
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
