using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
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

    [SerializeField] public float[] options;
    UtilitySystemEngine utilEngine;

    public bool optionPicked = false;

    public GameObject enemyPicked;
    int indexPicked;


    int movementH;

    public GridCreator gridCreator;
    public Carriage carro;

    bool isAttacking;
    public int selection;

    public bool isMoving;

    public GameObject telixFirst;
    public GameObject telixSecond;
    public GameObject vetrixObjective;
    public GameObject nibusSeed;


    public void SetStats(int level)
    {
        maxLivePoints = ps[(level % 3 != 0) ? (level / 3) + 1 : level / 3];
        livePoints = maxLivePoints;
        attack = att[(level % 3 != 0) ? (level / 3) + 1 : level / 3];
        defense = def[(level % 3 != 0) ? (level / 3) + 1 : level / 3];
        heatResistance = hRes[(level % 3 != 0) ? (level / 3) + 1 : level / 3];
        freezeResistance = fRes[(level % 3 != 0) ? (level / 3) + 1 : level / 3];
        agility = agt[(level % 3 != 0) ? (level / 3) + 1 : level / 3];
        maxMovement = mov[(level % 3 != 0) ? (level / 3) + 1 : level / 3];

    }
    private void Update()
    {
        if (startPanel.activeSelf)
        {
            return;
        }
        States();
        livePointsText.text = livePoints.ToString() + "/" + maxLivePoints.ToString();
    }
    //ESCARABAJO
    private new void Start()
    {
        MovementPoint = transform.position;
        for (int i = 0; i < skillPrefabs.Count; i++)
        {
            skillObjects.Add(GameObject.Instantiate(skillPrefabs[i], gameObject.transform));
            skills.Add(skillObjects[i].GetComponent<Skill>());
        }

        isMoving = false;
        isAttacking = false;
        gridCreator = GameObject.Find("Grid").GetComponent<GridCreator>();

        carro = GameObject.Find("carro(Clone)").GetComponent<Carriage>();
        isWalking = false;
        int movementH = maxMovement;
        options = new float[5];

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
                gottenSeed = false;
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
                if (!optionPicked)
                {
                    PickOption();
                }
                else
                {
                    actualState = EntityState.FINISHED;
                }

                break;
            case EntityState.MOVEING:
                Debug.Log("BUCLE MOVEING");
                //if (!isMoving)
                //  {//
                if (gottenSeed)
                    actualState = EntityState.FINISHED;
                if (path == null)
                {

                    path = PathFinding.instance.PathFind(thisTile, destination);
                    pathPosition = path.Count() - 1;
                    isMoving = false;
                    Move();
                }
                else
                {
                    isMoving = false;
                    Move();
                }
                


                // }
                break;
            case EntityState.ATTACKING:
                plants = GameController.instance.players;
                if (plants.Count == 0)
                {
                    actualState = EntityState.FINISHED;
                }
                if(name == "Orlaw" && moveAndAttack && skills[0].coolDown == 0)
                {
                    attacked = true;
                    skillSelected = 0;
                    skills[0].Effect(vetrixObjective, GameController.instance.SelectedPlayer());
                    Debug.Log("ORLAW HABILITY");
                    Debug.Log("ORLAW " + attack);
                    //actualState = EntityState.FINISHED;
                    //return;
                }

                Debug.Log("attacking");
                attacked = true;
                if (moveAndAttack)
                {
                    for (int i = 0; i < mainObjective.GetComponent<Entity>().skills.Count; i++)
                    {
                        if (mainObjective.GetComponent<Entity>().skills[i].isReciveingDamage)
                            mainObjective.GetComponent<Entity>().skills[i].Effect(gameObject, mainObjective);
                    }
                    mainAttack.Effect(mainObjective, gameObject);
                }
                moveAndAttack = false;
                if (attacked)
                {
                    return;
                }
                for (int i = 0; i < mainObjective.GetComponent<Entity>().skills.Count; i++)
                {
                    if (mainObjective.GetComponent<Entity>().skills[i].isReciveingDamage)
                        mainObjective.GetComponent<Entity>().skills[i].Effect(gameObject, mainObjective);
                }
                mainAttack.Effect(mainObjective, gameObject);
                if (isAttacking)
                {
                    actualState = EntityState.MOVEING;
                    isAttacking = false;
                }

                plants = GameController.instance.players;
                if (plants.Count == 0)
                    actualState = EntityState.FINISHED;

                // States();

                break;
            case EntityState.USINGSKILL:
                //GridCreator.instance.ShineTiles(gridX, gridY, skills[skillSelected].radious, false);
                //OPCION PARA VER SI EL INSECTO PUEDE USAR HABILIDAD
                //OPCION PARA VER EL COOLDWON
                //


                if (plants.Count == 0)
                {
                    actualState = EntityState.FINISHED;
                }
                if (name == "Telix")
                {

                    if (moveAndHability && skills[0].currentCoolDown == 0)
                    {
                        //  if (skills[skillSelected].actilveOnClick)
                        //{
                        attacked = true;
                        skillSelected = 0;
                        skills[0].Effect(telixSecond, GameController.instance.SelectedPlayer());
                        break;
                    }
                }
                else if (name == "Vetrix")
                {
                    if (skills[0].currentCoolDown == 0 && moveAndHability)
                    {
                        Debug.Log("VETRIX");
                        attacked = true;
                        skillSelected = 0;
                        skills[0].Effect(vetrixObjective, GameController.instance.SelectedPlayer());
                        break;
                    }
                }
                else if (name == "Nibus")
                {
                    if (gottenSeed)
                    {
                        actualState = EntityState.FINISHED;
                    }
                    else
                    {
                        attacked = true;
                        skills[0].Effect(nibusSeed, GameController.instance.SelectedPlayer());
                    }
                }

                //

                /* if (skills[skillSelected].actilveOnClick)
             {
                 skills[skillSelected].Effect(gameObject, GameController.instance.SelectedPlayer());
             }
             else
             {
                 GridCreator.instance.SearchObjective(gridX, gridY, skills[skillSelected].range, false);
             }*/
                break;
            case EntityState.FINISHED:
                GameController.instance.NextPlayer();
                optionPicked = false;

                break;

        }






        // }
    }

    public GameObject CustomSetDestination(GameObject tile)
    {
        Debug.Log("Bucle CUSTOM");
        GetComponent<Entity>().actualState = Entity.EntityState.MOVEING;
        destination = tile;
        if (destination.GetComponent<Tile>() != null)
            return null;
        if (destination.GetComponent<Tile>().isWalkable)
            return tile;
        else
            return null;
    }

    public GameObject CustomSetDestinationC(GameObject tile)
    {
        Debug.Log("Bucle CUSTOM");
        GetComponent<Entity>().actualState = Entity.EntityState.MOVEING;
        destination = tile;
        if (destination.GetComponent<Tile>() == null)
            return null;
        if (destination.GetComponent<Tile>().isWalkable)
            return tile;
        else
            return null;
    }

    public void PickOption()
    {
        carro = GameObject.Find("carro(Clone)").GetComponent<Carriage>();
        //Attack Carro
        List<Factor> factorsAttackCarro = new List<Factor>();

        Factor minTwenty = new LeafVariable(() => 0.2f, 1, 0);

        List<Factor> factorsHealth = new List<Factor>();
        Factor _healthCarroAbs = new LeafVariable(() => carro.livePoints, 1, carro.maxLivePoints);
        factorsHealth.Add(minTwenty);
        factorsHealth.Add(_healthCarroAbs);

        List<Factor> factorsFinalDist = new List<Factor>();
        Factor _finalDistCarroAbs = new LeafVariable(() => carro.gridY, gridCreator.y, 1);
        factorsFinalDist.Add(minTwenty);
        factorsFinalDist.Add(_finalDistCarroAbs);

        Factor _distCarro = new LeafVariable(() => distSmthToPlant(carro), gridCreator.y + gridCreator.x, 1);

        Factor _healthCarro = new MaxFusion(factorsHealth);


        Factor _finalDistCarro = new MaxFusion(factorsFinalDist);

        factorsAttackCarro.Add(_healthCarro);
        factorsAttackCarro.Add(_finalDistCarro);
        factorsAttackCarro.Add(_distCarro);

        // List<float> valuesCarro = new List<float>();
        //valuesCarro.Add(_healthCarro.getValue());
        List<float> weightsC = new List<float>();
        weightsC.Add(0.30f);
        weightsC.Add(0.45f);
        weightsC.Add(0.25f);

        Factor weightAtC = new WeightedSumFusion(factorsAttackCarro, weightsC);


        float valueHealth = _healthCarro.getValue();
        float valueFinalDistC = _finalDistCarro.getValue();
        float valueDistC = _distCarro.getValue();



        Debug.Log("HEAL CARRO:  " + _healthCarro.getValue());
        Debug.Log("FINAL DIST CARRO:  " + _finalDistCarro.getValue());
        Debug.Log("DIST CARRO:  " + _distCarro.getValue());
        Debug.Log("CARRO PESADO: " + weightAtC.getValue());

        options[0] = weightAtC.getValue();

        options[0] += 0.1f;


        //Imp Mov Carro
        options[1] = 0;

        //Huir Planta
        options[2] = 0.05f;

        int index = 0;
        //Atacar Planta X
        if (plants.Count != 0)
        {


            float[] plantsAtt = new float[plants.Count];
            //  float esRent = CalcRentable(plantsInfo[i]);
            for (int i = 0; i < plants.Count; i++)
            {
                List<Factor> factors = new List<Factor>();
                Bichous test = gameObject.GetComponent<Bichous>();
                Plants tes2 = plants[i].GetComponent<Plants>();
                Debug.Log(plants[i].name);
                plantsInfo[i].Init(plants[i].GetComponent<Plants>(), gameObject.GetComponent<Bichous>());//
                //plantsInfo[i].UpdateRelevant();

                Factor _distToPlantsLeafHelper1 = new LeafVariable(() => plantsInfo[i].distToPlant, 1, 33);
                //  Factor _distToPlantsLeafHelper2 = new LeafVariable(() => plantsInfo[1].distToPlant, 33, 1);

                Factor esRent = new LeafVariable(() => CalcRentable(plantsInfo[i]), 33, 1);

                factors.Add(_distToPlantsLeafHelper1);

                factors.Add(esRent);
                // Pesos
                List<float> weightsAP = new List<float>();
                weightsAP.Add(0.4f);
                weightsAP.Add(0.6f);
                // Weighted Sum 2 opciones
                Factor weightFactor = new WeightedSumFusion(factors, weightsAP);
                plantsAtt[i] = weightFactor.getValue();

                Debug.Log(plantsAtt[i] + " " + plantsInfo[i].name);
            }
            index = Array.IndexOf(plantsAtt, plantsAtt.Max());

            // options.Add(plantsAtt[index]);
            options[3] = plantsAtt[index];
        }
        else
            options[3] = 0;

        //HABILIDADES

        //tenemos el enemigo mas cercano que tiene al lado a otro enemigo
        //el numero de enemigos que estan cercanos entre ellos
        //la distancia a el enemigo
        //    List<GameObject> plantsCarro = new List<GameObject>(plants);

        if (skills[0].currentCoolDown == 0)
        {

            if (name == "Telix")
            {
                int numP = TelixHability();
                float pesoNumP = 0;
                if (numP == -1)
                {
                    options[4] = 0;
                }
                if (numP == 0)
                    pesoNumP = 0.3f;
                if (numP == 1)
                    pesoNumP = 0.7f;
                if (numP == 2)
                    pesoNumP = 1f;
                if (telixFirst != null && telixSecond != null)
                {
                    Factor _distToPlant = new LeafVariable(() => distSmthToSmthVariables(telixFirst.GetComponent<Tile>().positionX, telixFirst.GetComponent<Tile>().positionY, gridX, gridY), 33, 1);
                  
                    options[4] = ((float)(0.3 * _distToPlant.getValue() + 0.7 * pesoNumP));
                }
                else
                    options[4] = 0;
            }
            else if(name == "Vetrix")
            {
                Debug.Log(skills[0].currentCoolDown);
                (float pesoVetrix,int indiceVetrix) = VetrixHability();
                options[4] = pesoVetrix + 0.2f;
            }
            else if(name == "Nibus")
            {
                float pesoNibus = NibusHability();
                options[4] = pesoNibus;
            }
        }
        else
            options[4] = 0;


        float maxValue = options.Max();
        selection = options.ToList().IndexOf(maxValue);

        //GameController.instance.SelectedPlayer().GetComponent<Plants>().mainObjective = gameObject;
        //GameController.instance.SelectedPlayer().GetComponent<Plants>().actualState = Entity.EntityState.ATTACKING;

        ExecuteSelected(selection, index);
        GameController.instance.NextPlayer();
        optionPicked = true;
        return;

    }

    public void ExecuteSelected(int decision, int indPlanta)
    {
        plants = GameController.instance.players;
        if (plants.Count != 0)
        {
            enemyPicked = plants[indPlanta];
            indexPicked = indPlanta;
        }

        if (decision == 0)
        {
            //Atack Carro
            //IGUAL QUE ATACAR A PLANTA PERO AL CARRO

            Carriage falseCarro = new Carriage();
            falseCarro.gridX = carro.gridX;
            falseCarro.gridY = carro.gridY;

            if (gameObject.GetComponent<Bichous>().movementH != 0 && !attacked)
            {
                // plantsInfo[indPlanta].Init(plants[indPlanta].GetComponent<Plants>(), gameObject.GetComponent<Bichous>());
                if (distSmthToPlant(carro) <= 2) // 1 o range
                {
                    //Attack
                    //  GameController.instance.SelectedPlayer().GetComponent<Bichous>().mainObjective = GridCreator.instance.GetTile(carro.GetComponent<Entity>().gridX, carro.GetComponent<Entity>().gridY).GetComponent<Tile>().entity;
                    if (!attacked)
                    {
                        mainObjective = carro.gameObject;
                        GameController.instance.SelectedPlayer().GetComponent<Bichous>().actualState = Entity.EntityState.ATTACKING;

                        //Mosquitoes?
                        // attacked = true;
                        
                        attacked = true;
                        for (int i = 0; i < mainObjective.GetComponent<Entity>().skills.Count; i++)
                        {
                            if (mainObjective.GetComponent<Entity>().skills[i].isReciveingDamage)
                                mainObjective.GetComponent<Entity>().skills[i].Effect(gameObject, mainObjective);
                        }
                        mainAttack.Effect(mainObjective, gameObject);
                    }
                }
                else
                {
                    (int newX, int newY) = MoveToClosePositionC(carro);
                    //attacked = true;

                    // plantsInfo[indPlanta].Init(plants[indPlanta].GetComponent<Plants>(), gameObject.GetComponent<Bichous>());
                    Carriage falseCarro2 = new Carriage();

                    falseCarro2.gridX = newX;
                    falseCarro2.gridY = newY;

                    moveAndAttack = false;

                    float a = distSmthToSmthVariables(newX, newY, carro.gridX, carro.gridY);
                    if (distSmthToSmthVariables(newX, newY, carro.gridX, carro.gridY) <= 2) // 1 o range
                    {

                        //  GameController.instance.SelectedPlayer().GetComponent<Bichous>().mainObjective = GridCreator.instance.GetTile(enemyPicked.GetComponent<Entity>().gridX, enemyPicked.GetComponent<Entity>().gridY).GetComponent<Tile>().entity;

                        GameController.instance.SelectedPlayer().GetComponent<Bichous>().mainObjective = carro.gameObject;
                        GameController.instance.SelectedPlayer().GetComponent<Bichous>().actualState = Entity.EntityState.MOVEING;

                        moveAndAttack = true;



                    }
                }

            }


            return;

        }
        else if (decision == 1)
        {
            //Impedir Mov Carro

        }
        else if (decision == 2)
        {
            //Huir de planta
        }
        //Si no es ninguno de estos sera atacar a una planta
        else if (decision == 3)
        {

            if (gameObject.GetComponent<Bichous>().movementH != 0 && !attacked)
            {
                plantsInfo[indPlanta].Init(plants[indPlanta].GetComponent<Plants>(), gameObject.GetComponent<Bichous>());
                if (plantsInfo[indPlanta].distToPlant <= 2) // 1 o range
                {
                    //Attack
                    GameController.instance.SelectedPlayer().GetComponent<Bichous>().mainObjective = GridCreator.instance.GetTile(enemyPicked.GetComponent<Entity>().gridX, enemyPicked.GetComponent<Entity>().gridY).GetComponent<Tile>().entity;
                    GameController.instance.SelectedPlayer().GetComponent<Bichous>().actualState = Entity.EntityState.ATTACKING;
                    //mosquitoes?
                    // attacked = true;
                  
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
                    (int newX, int newY) = MoveToClosePosition(plantsInfo[indPlanta]);
                    //attacked = true;

                    // plantsInfo[indPlanta].Init(plants[indPlanta].GetComponent<Plants>(), gameObject.GetComponent<Bichous>());
                    plantsInfo[indPlanta].GridX = newX;
                    plantsInfo[indPlanta].GridY = newY;
                    plantsInfo[indPlanta].UpdateRelevant();

                    int a = distSmthToSmthVariables(newX, newY, plants[indPlanta].GetComponent<Plants>().gridX, plants[indPlanta].GetComponent<Plants>().gridY);
                    if (a <= 2) // 1 o range
                    {
                        //Attack
                        GameController.instance.SelectedPlayer().GetComponent<Bichous>().mainObjective = GridCreator.instance.GetTile(enemyPicked.GetComponent<Entity>().gridX, enemyPicked.GetComponent<Entity>().gridY).GetComponent<Tile>().entity;                    //  attacked = true;
                                                                                                                                                                                                                                                                          //GameController.instance.SelectedPlayer().GetComponent<Mosquitoes>().actualState = Entity.EntityState.ATTACKING;

                        GameController.instance.SelectedPlayer().GetComponent<Bichous>().actualState = Entity.EntityState.MOVEING;

                        moveAndAttack = true;



                    }
                    /*
                    if (plantsInfo[indPlanta].distToPlant <= 2) // 1 o range
                {
                    //Attack
                    GameController.instance.SelectedPlayer().GetComponent<Bichous>().mainObjective = GridCreator.instance.GetTile(enemyPicked.GetComponent<Entity>().gridX, enemyPicked.GetComponent<Entity>().gridY).GetComponent<Tile>().entity;                    //  attacked = true;
                    //GameController.instance.SelectedPlayer().GetComponent<Mosquitoes>().actualState = Entity.EntityState.ATTACKING;
                    GameController.instance.SelectedPlayer().GetComponent<Bichous>().actualState = Entity.EntityState.MOVEING;

                    moveAndAttack = true;

                    /* attacked = true;
                     for (int i = 0; i < mainObjective.GetComponent<Entity>().skills.Count; i++)
                     {
                         if (mainObjective.GetComponent<Entity>().skills[i].isReciveingDamage)
                             mainObjective.GetComponent<Entity>().skills[i].Effect(gameObject, mainObjective);
                     }
                     mainAttack.Effect(mainObjective, gameObject);

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

                     States();*/
                }
            }

        }
        else if (decision == 4)
        {

            if (gameObject.GetComponent<Bichous>().movementH != 0 && !attacked)
            {
                if (name == "Telix")
                {
                    (int newX, int newY) = MoveToClosePositionH(telixFirst.GetComponent<Tile>());
                    moveAndAttack = false;

                    float a = distSmthToSmthVariables(newX, newY, carro.gridX, carro.gridY);
                    if (distSmthToSmthVariables(newX, newY, telixFirst.GetComponent<Tile>().positionX, telixFirst.GetComponent<Tile>().positionY) <= 2) // 1 o range
                    {


                        GameController.instance.SelectedPlayer().GetComponent<Bichous>().actualState = Entity.EntityState.MOVEING;

                        moveAndHability = true;

                    }
                }else if(name == "Vetrix")
                {

                    // GameController.instance.SelectedPlayer().GetComponent<Bichous>().actualState = Entity.EntityState.USINGSKILL;
                    Tile vetrixTile = GridCreator.instance.GetTile(vetrixObjective.GetComponent<Entity>().gridX, vetrixObjective.GetComponent<Entity>().gridY).GetComponent<Tile>();
                    (int newX, int newY) = MoveToClosePositionH(vetrixTile);


                    moveAndHability = false;
                    if (distSmthToSmthVariables(newX, newY, vetrixTile.positionX, vetrixTile.positionY) <= 2)
                    {
                        GameController.instance.SelectedPlayer().GetComponent<Bichous>().actualState = Entity.EntityState.MOVEING;
                        moveAndHability = true;
                    }// 1 o range


                }
                else if(name == "Nibus")
                {
                    // SetDestination(nibusSeed);
                    (int newX, int newY) = MoveToClosePositionH(nibusSeed.GetComponent<Tile>());

                    GameController.instance.SelectedPlayer().GetComponent<Bichous>().actualState = Entity.EntityState.MOVEING;

                }

            }
        }



    }
    
    public (int, int) MoveToClosePosition(RelevantInfoPlantInsect plantaObj)
    {
        GameObject dest = null;

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
     
    
        if (CustomSetDestinationC(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY)) != null)
            dest = CustomSetDestinationC(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY));

      /*  else if (CustomSetDestinationC(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (plantaObj.GridY + 1 < 0 || plantaObj.GridY + 1> GridCreator.instance.height) ? plantaObj.GridY + 1 : plantaObj.GridY + 1)) != null)
        {
            try
            {
                dest = CustomSetDestinationC(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (plantaObj.GridY + 1 < 0 || plantaObj.GridY + 1 > GridCreator.instance.height) ? plantaObj.GridY + 1 : plantaObj.GridY + 1));
                movedY = plantaObj.GridY + 1;
            }
            catch { }
        }
        else if (CustomSetDestinationC(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (plantaObj.GridY - 1 < 0 || plantaObj.GridY - 1 > GridCreator.instance.height) ? plantaObj.GridY - 1 : plantaObj.GridY - 1)) != null)
        {
            try { 
            dest = CustomSetDestinationC(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (plantaObj.GridY - 1 < 0 || plantaObj.GridY - 1 > GridCreator.instance.height) ? plantaObj.GridY - 1 : plantaObj.GridY - 1));
            movedY = plantaObj.GridY - 1;
            }
            catch { }
        }
        else if (CustomSetDestinationC(GridCreator.instance.GetTile((plantaObj.GridX + 1 < 0 || plantaObj.GridX + 1 > GridCreator.instance.width) ? plantaObj.GridX + 1 : plantaObj.GridX + 1, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY)) != null)
        {
            try { 
            dest = CustomSetDestinationC(GridCreator.instance.GetTile((plantaObj.GridX + 1 < 0 || plantaObj.GridX + 1 > GridCreator.instance.width) ? plantaObj.GridX + 1 : plantaObj.GridX + 1, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY));
            movedX = plantaObj.GridX + 1;
            }
            catch { }
        }
        else if (CustomSetDestinationC(GridCreator.instance.GetTile((plantaObj.GridX - 1 < 0 || plantaObj.GridX - 1 > GridCreator.instance.width) ? plantaObj.GridX - 1 : plantaObj.GridX - 1, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY)) != null)
        {
            try { 
            dest = CustomSetDestinationC(GridCreator.instance.GetTile((plantaObj.GridX - 1 < 0 || plantaObj.GridX - 1 > GridCreator.instance.width) ? plantaObj.GridX - 1 : plantaObj.GridX - 1, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY));
            movedX = plantaObj.GridX - 1;
                }
            catch { }
        }
      */
        int movedXi = movedX;
        int movedYi = movedY;
        int bucles = 0;
        while (dest == null)
        {
            bucles++;
            movedXi++;
            movedYi--;
            try
            {
                if (GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi) != null)
                    dest = CustomSetDestinationC(GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi));
                if(dest != null)
                {
                    movedX = movedXi;
                    movedY = movedYi;
                }
            }
            catch
            {
                break;
            }
            if (bucles == 30)
            {
               
                    for (int i = 0; i < 100; i++)
                    {
                        movedXi--;
                        movedYi++;
                        try
                        {
                            if (CustomSetDestinationC(GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi)) != null)
                                dest = CustomSetDestinationC(GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi));
                            if (dest != null)
                            {
                                movedX = movedXi;
                                movedY = movedYi;
                            }
                        }
                        catch
                        {
                            break;
                        }
                        if (i == 100)
                        {
                            break;

                        }
                    }
            }
            if(bucles == 200)
            {
                MovementPoint = transform.position;
                moveing = false;
                path = null;
                return (gridX, gridY);
                Debug.Log("EMERGENCY BREAK");
                break;

            }
        }
        MovementPoint = transform.position;
        moveing = false;
        path = null;
        return (movedX, movedY);



    }


    public (int, int) MoveToClosePositionH(Tile plantaObj)
    {
        GameObject dest = null;

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
            if (Mathf.Abs(movedX - plantaObj.positionX) == 0)
                contX = true;
            if (!contX)
            {
                //destX =  -Mathf.Clamp(Mathf.RoundToInt((int)plantaObj.GridX), -1, 1);
                if (plantaObj.positionX > movedX)
                    destX = 1;
                else
                    destX = -1;
                movedX += destX;
                movementH -= 1;
                // movement -= 1;


            }
            if (Mathf.Abs(movedY - plantaObj.positionY) == 0)
                contY = true;
            if (!contY)
            {
                //destY = -Mathf.Clamp(Mathf.RoundToInt((int)plantaObj.GridY), -1, 1);
                if (plantaObj.positionX > movedY)
                    destY = 1;
                else
                    destY = -1;
                movedY += destY;
                movementH -= 1;
                // movement -= 1;
            }
        }



        // if (CustomSetDestinationC(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY)) != null)
        //dest = CustomSetDestinationC(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY));
        try
        {
            dest = CustomSetDestinationC(GridCreator.instance.GetTile(movedX,movedY));
        }
        catch
        {
            dest = null;
        }
        int movedXi = movedX;
        int movedYi = movedY;
        int bucles = 0;
        while (dest == null)
        {
            bucles++;
            movedXi++;
            movedYi--;
            try
            {
                if (GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi) != null)
                    dest = CustomSetDestinationC(GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi));
                if (dest != null)
                {
                    movedX = movedXi;
                    movedY = movedYi;
                }
            }
            catch
            {
                break;
            }
            if (bucles == 30)
            {

                for (int i = 0; i < 100; i++)
                {
                    movedXi--;
                    movedYi++;
                    try
                    {
                        if (CustomSetDestinationC(GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi)) != null)
                            dest = CustomSetDestinationC(GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi));
                        if (dest != null)
                        {
                            movedX = movedXi;
                            movedY = movedYi;
                        }
                    }
                    catch
                    {
                        break;
                    }
                    if (i == 100)
                    {
                        break;

                    }
                }
            }
            if (bucles == 200)
            {
                MovementPoint = transform.position;
                moveing = false;
                path = null;
                return (gridX, gridY);
                Debug.Log("EMERGENCY BREAK");
                break;

            }
        }
        MovementPoint = transform.position;
        moveing = false;
        path = null;
        return (movedX, movedY);



    }
    /*
    public (int, int) MoveToClosePosition(Entity End)
    {
        GameObject dest = null;
        //PriorityQueue<float, float> OpenList = new PriorityQueue<float, float>();
        int destX = 0;
        int destY = 0;

        //int xNeeded = gridX - (int)plantaObj.distX;
        // yNeeded = gridY - (int)plantaObj.distY;

        int movedX = 0;
        int movedY = 0;

        bool contX = false;
        bool contY = false;

        movementH = maxMovement;

        List<GameObject> posNodes = new List<GameObject>();


    }
    
    public void FindTilesInRange()
    {
        List<NodePos> temp = new List<NodePos>();

        int row = gridX;
        int col = gridY;

        int temp1Row = gridX;
        int temp2Row = gridX;

        int temp1Col = gridX;
        int temp2Col = gridX;

        for (int i = 0; i < maxMovement; i++)
        {
            if (temp1Row + 1 < gridCreator.y)
            {
                // temp.Add(Grid[col][row + 1]);
                int tempX = GridCreator.instance.GetTile(col, temp1Row + 1).GetComponent<Tile>().positionX;
                int tempY = GridCreator.instance.GetTile(col, temp1Row + 1).GetComponent<Tile>().positionY;
                NodePos tempNode = new NodePos(tempX, tempY);

                temp.Add(tempNode);
                temp1Row++;
            }
        }
            if (temp2Row - 1 >= 0)
            {
                int tempX = GridCreator.instance.GetTile(col, temp2Row - 1).GetComponent<Tile>().positionX;
                int tempY = GridCreator.instance.GetTile(col, temp2Row - 1).GetComponent<Tile>().positionY;
                NodePos tempNode = new NodePos(tempX, tempY);

                temp.Add(tempNode);
            temp2Row--;
            }
            if (temp1Col - 1 >= 0)
            {
                int tempX = GridCreator.instance.GetTile(temp1Col - 1, row).GetComponent<Tile>().positionX;
                int tempY = GridCreator.instance.GetTile(temp1Col - 1, row).GetComponent<Tile>().positionY;
                NodePos tempNode = new NodePos(tempX, tempY);

                temp.Add(tempNode);
            }
            if (temp2Col + 1 < gridCreator.x)
            {
                int tempX = GridCreator.instance.GetTile(temp2Col + 1, row).GetComponent<Tile>().positionX;
                int tempY = GridCreator.instance.GetTile(temp2Col + 1, row).GetComponent<Tile>().positionY;
                NodePos tempNode = new NodePos(tempX, tempY);

                temp.Add(tempNode);
            }

        for (int i = 0; i < maxMovement-1; i++)
        {
            if (temp1Row + 1 < gridCreator.y)
            {
                // temp.Add(Grid[col][row + 1]);
                int tempX = GridCreator.instance.GetTile(col-1, temp1Row + 1).GetComponent<Tile>().positionX;
                int tempY = GridCreator.instance.GetTile(col-1, temp1Row + 1).GetComponent<Tile>().positionY;
                NodePos tempNode = new NodePos(tempX, tempY);

                temp.Add(tempNode);
                temp1Row++;
            }
        }
        for (int i = 0; i < maxMovement - 1; i++)
        {
            if (temp2Row - 1 >= 0)
            {
                // temp.Add(Grid[col][row + 1]);
                int tempX = GridCreator.instance.GetTile(temp1Col - 1, row-1).GetComponent<Tile>().positionX;
                int tempY = GridCreator.instance.GetTile(temp1Col - 1, row-1).GetComponent<Tile>().positionY;
                NodePos tempNode = new NodePos(tempX, tempY);

                temp.Add(tempNode);
                temp1Row++;
            }
        }
        for (int i = 0; i < maxMovement - 1; i++)
        {
            if (temp1Row + 1 < gridCreator.y)
            {
                // temp.Add(Grid[col][row + 1]);
                int tempX = GridCreator.instance.GetTile(temp1Col - 1, row - 1).GetComponent<Tile>().positionX;
                int tempY = GridCreator.instance.GetTile(temp1Col - 1, row - 1).GetComponent<Tile>().positionY;
                NodePos tempNode = new NodePos(tempX, tempY);

                temp.Add(tempNode);
                temp1Row++;
            }
        }



        return temp;

    }
    */

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

    public void Attack()
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
        if (GameController.instance.SelectedPlayer().GetComponent<Plants>().attacked)
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

            GameController.instance.SelectedPlayer().GetComponent<Plants>().skills[GameController.instance.SelectedPlayer().GetComponent<Plants>().skillSelected].Effect(mainObjective, GameController.instance.SelectedPlayer());
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
        switch (collision.tag)
        {
            case "Proyectil":
                livePoints -= collision.gameObject.GetComponentInParent<Plants>().mainAttack.DamageCalculator(gameObject.GetComponent<Bichous>(), collision.gameObject.GetComponentInParent<Plants>());
                //mosquitoes?
                print(livePoints);
                break;
        }
    }

    public int distSmthToPlant(Entity who)
    {
        int distX = Mathf.Abs(who.gridX - gridX);
        int distY = Mathf.Abs(who.gridY - gridY);

        int distTo = (distX + distY);
        int distToPlant = distTo;

        return distToPlant;
    }

    public int distSmthToSmthVariables(int gX, int gY, int gX1, int gY1)
    {
        int distX = Mathf.Abs(gX - gX1);
        int distY = Mathf.Abs(gY - gY1);

        int distTo = (distX + distY);
        int distToPlant = distTo;

        return distToPlant;
    }


    //REDUNDANTE QUITAR
    //IGUAL QUE EL METODO DE PLANTAS

    public (int, int) MoveToClosePositionC(Carriage plantaObj)
    {
        GameObject dest = null;

        //TO DO
        int destX = 0;
        int destY = 0;


        int movedX = 0;
        int movedY = 0;

        bool contX = false;
        bool contY = false;

        movementH = maxMovement;

        movedX = gridX + destX;
        movedY = gridY + destY;

        while (movementH > 0 && (contX && contY) == false)
        {
            if (Mathf.Abs(movedX - plantaObj.gridX) < 2)
                contX = true;
            if (!contX)
            {
                if (plantaObj.gridX > movedX)
                    destX = 1;
                else
                    destX = -1;
                movedX += destX;
                movementH -= 1;


            }
            if (Mathf.Abs(movedY - plantaObj.gridY) < 2)
                contY = true;
            if (!contY)
            {
                if (plantaObj.gridY > movedY)
                    destY = 1;
                else
                    destY = -1;
                movedY += destY;
                movementH -= 1;
            }
        }

        /*
        Debug.Log(movedX + ", " + movedY);
        Debug.Log(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY));


        if (contX && contY && !attacked)
        {
            Debug.Log("VA A MOVE AND ATACAR");
        }

        SetDestination(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY));
        MovementPoint = transform.position;
        moveing = false;
        path = null;
        return (movedX, movedY);
        */

        if (CustomSetDestinationC(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY)) != null)
            dest = CustomSetDestinationC(GridCreator.instance.GetTile((movedX < 0 || movedX > GridCreator.instance.width) ? movedX : movedX, (movedY < 0 || movedY > GridCreator.instance.height) ? movedY : movedY));
        int movedXi = movedX;
        int movedYi = movedY;
        int bucles = 0;
        while (dest == null)
        {
            bucles++;
            movedXi++;
            movedYi--;
            try
            {
                if (GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi) != null)
                    dest = CustomSetDestinationC(GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi));
                if (dest != null)
                {
                    movedX = movedXi;
                    movedY = movedYi;
                }
            }
            catch 
            {
                break;
            }
                
            
                if (bucles == 30)
                {

                    for (int i = 0; i < 100; i++)
                    {
                        movedXi--;
                        movedYi++;
                        try
                        {
                            if (CustomSetDestinationC(GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi)) != null)
                                dest = CustomSetDestinationC(GridCreator.instance.GetTile((movedXi < 0 || movedXi > GridCreator.instance.width) ? movedXi : movedXi, (movedYi < 0 || movedYi > GridCreator.instance.height) ? movedYi : movedYi));
                            if (dest != null)
                            {
                                movedX = movedXi;
                                movedY = movedYi;
                            }
                        }
                        catch
                        {
                            break;
                        }
                        if (i == 100)
                        {
                            break;

                        }
                    }
                }
                if (bucles == 200)
                {
                    MovementPoint = transform.position;
                    moveing = false;
                    path = null;
                    return (gridX, gridY);
                    Debug.Log("EMERGENCY BREAK");

                    break;

                }
            }
        
        MovementPoint = transform.position;
        moveing = false;
        path = null;
        return (movedX, movedY);

    }

    public float NibusHability()
    {
        List<GameObject> seeds = new List<GameObject>();
       
        for (int i = 0; i < GridCreator.instance.seeds.Count; i++)
        {
            if(GridCreator.instance.seeds[i] != null)
            seeds.Add(GridCreator.instance.seeds[i].transform.parent.gameObject);
        }
        float[] SeedsWeights = new float[seeds.Count];
        if (seeds.Count == 0)
            return 0;
        for (int i = 0; i < seeds.Count; i++)
        {
            Factor _distSeedsToPlants = new LeafVariable(() => distSmthToSmthVariables(seeds[i].GetComponent<Tile>().positionX, seeds[i].GetComponent<Tile>().positionY, gridX, gridY), 1, 26);
            SeedsWeights[i] = _distSeedsToPlants.getValue();
        }
        int indexRes = Array.IndexOf(SeedsWeights, SeedsWeights.Max());
        float result = SeedsWeights[indexRes];
        nibusSeed = seeds[indexRes];
        return result;
    }

    //Habilidades insecto
    public (float,int) VetrixHability()
    {
        List<GameObject> plantsInsects = new List<GameObject>(plants);
        

        for (int i = 0; i< GameController.instance.enemys.Count; i++)
        {
            if (GameController.instance.enemys[i].name != "Vetrix") ;
                plantsInsects.Add(GameController.instance.enemys[i]);
        }
        float[] plantsInsectsWeights = new float[plantsInsects.Count];
        
        //PLANTAS
        for(int i = 0; i < plants.Count; i++)
        {
            List<Factor> factors = new List<Factor>();

            Factor _distToPlantsLeafHelper1 = new LeafVariable(() => distSmthToSmthVariables(plantsInsects[i].GetComponent<Entity>().gridX, plantsInsects[i].GetComponent<Entity>().gridX, gridX, gridY), 33, 1);

            Factor _healthInsect = new LeafVariable(() => plantsInsects[i].GetComponent<Entity>().livePoints, 1, plantsInsects[i].GetComponent<Entity>().maxLivePoints);


            factors.Add(_distToPlantsLeafHelper1);
            factors.Add(_healthInsect);
            List<float> weightsInsects = new List<float>();

            weightsInsects.Add(0.5f);
            weightsInsects.Add(0.5f);

            Factor weightFactor = new WeightedSumFusion(factors, weightsInsects);

            plantsInsectsWeights[i] = weightFactor.getValue();
        }
        //INSECTOS
        for (int i = plants.Count; i < plantsInsects.Count; i++)
        {
            List<Factor> factors = new List<Factor>();

            Factor _distToPlantsLeafHelper1 = new LeafVariable(() => distSmthToSmthVariables(plantsInsects[i].GetComponent<Entity>().gridX, plantsInsects[i].GetComponent<Entity>().gridX, gridX, gridY), 33, 1);
            Factor _healthInsect = new LeafVariable(() => plantsInsects[i].GetComponent<Entity>().livePoints, 1, plantsInsects[i].GetComponent<Entity>().maxLivePoints);

            factors.Add(_distToPlantsLeafHelper1);
            factors.Add(_healthInsect);
            List<float> weightsInsects = new List<float>();

            weightsInsects.Add(0.5f);
            weightsInsects.Add(0.5f);

            Factor weightFactor = new WeightedSumFusion(factors, weightsInsects);

            plantsInsectsWeights[i] = weightFactor.getValue();
        }
        int indexRes = Array.IndexOf(plantsInsectsWeights, plantsInsectsWeights.Max());
        float result = plantsInsectsWeights[indexRes];
        vetrixObjective = plantsInsects[indexRes];
        return (result, indexRes);

    }


    public int TelixHability()
    {
        List<GameObject> plantsCarro = new List<GameObject>(plants);
        plantsCarro.Add(carro.gameObject);
        int[,] cercania = new int[plantsCarro.Count, plantsCarro.Count];
        int closeEnemies = 0;
        GameObject closer = null;
        GameObject last = null;
        int[] distPlant = new int[plantsCarro.Count];
        for (int i = 0; i < plantsCarro.Count; i++)
        {
            distPlant[i] = distSmthToSmthVariables(gridX, gridY, plantsCarro[i].GetComponent<Entity>().gridX, plantsCarro[i].GetComponent<Entity>().gridX);
            for (int j = 0; j < plantsCarro.Count; j++)
            {
                Bichous test = gameObject.GetComponent<Bichous>();
                Entity planta1 = plantsCarro[i].GetComponent<Entity>();
                Entity planta2 = plantsCarro[j].GetComponent<Entity>();

                cercania[i, j] = distSmthToSmthVariables(planta1.gridX, planta1.gridY, planta2.gridX, planta2.gridY);
                if (cercania[i, j] == 0)
                    cercania[i, j] = -1;
                Debug.ClearDeveloperConsole();
                Debug.Log("DISTANCIA " + planta1.name + " " + planta2.name + " " + cercania[i, j] );
                Debug.Log(cercania[i, j]);
                if (cercania[i, j] == 1)
                {
                    closeEnemies++;
                }

            }
        }
        int helper = 99;
        int notI = 0;
        for (int i = 0; i < plantsCarro.Count; i++)
        {
            for (int j = 0; j < plantsCarro.Count; j++)
            {
                if (cercania[i, j] == 2)
                    closer = plantsCarro[i];
                if (cercania[i, j] == 1 && distPlant[i] < helper)
                {
                    closer = plantsCarro[i];
                    notI = i;
                    helper = cercania[i, j];
                    if (closeEnemies == 6)
                    {
                        for (int m = 0; m < plantsCarro.Count; m++)
                        {
                            if (cercania[j, m] == 1)
                                last = plantsCarro[m];

                        }
                    }
                    if (closeEnemies == 2)
                    {
                        if (notI == 0)
                            last = plantsCarro[1];
                        else
                            last = plantsCarro[0];
                    }
                /*    if (closeEnemies == 4)
                    {
                        if (notI == 0)
                            last = plantsCarro[1];
                        else
                            last = plantsCarro[0];
                    }*/
                }
            }
        }

        try
        {
            if(closer.GetComponent<Entity>().gridX == last.GetComponent<Entity>().gridX)
            {
                //x es igual
                //y es distinto (mayor o menor?)
                if (closer.GetComponent<Entity>().gridY - last.GetComponent<Entity>().gridY < 0)
                {
                    telixFirst = GridCreator.instance.GetTile(closer.GetComponent<Entity>().gridX, closer.GetComponent<Entity>().gridY - 1);
                    telixSecond = GridCreator.instance.GetTile(closer.GetComponent<Entity>().gridX, last.GetComponent<Entity>().gridY + 1);
                }
                else
                {
                    telixFirst = GridCreator.instance.GetTile(closer.GetComponent<Entity>().gridX, closer.GetComponent<Entity>().gridY + 1);
                    telixSecond = GridCreator.instance.GetTile(closer.GetComponent<Entity>().gridX, last.GetComponent<Entity>().gridY - 1);

                }
            }
            else if(closer.GetComponent<Entity>().gridY == last.GetComponent<Entity>().gridY)
            {
                if (closer.GetComponent<Entity>().gridX - last.GetComponent<Entity>().gridX < 0)
                {
                    telixFirst = GridCreator.instance.GetTile(closer.GetComponent<Entity>().gridX - 1, closer.GetComponent<Entity>().gridY);
                    telixSecond = GridCreator.instance.GetTile(last.GetComponent<Entity>().gridX + 1, closer.GetComponent<Entity>().gridY);

                }
                else
                {
                    telixFirst = GridCreator.instance.GetTile(closer.GetComponent<Entity>().gridX + 1, closer.GetComponent<Entity>().gridY);
                    telixSecond = GridCreator.instance.GetTile(last.GetComponent<Entity>().gridX - 1, closer.GetComponent<Entity>().gridY);

                }
            }
        //    if(!telixFirst.GetComponent<Tile>().isWalkable || !telixSecond.GetComponent<Tile>().isWalkable)
          //      return -1
        }
        catch
        {
            return -1;
        }

            //PathFinding.instance.CustomPathShine(GridCreator.instance.GetTile(gridX, gridY));
       // if(closer != null)
         //   telixFirst = closer.gameObject;
         if(!telixFirst.GetComponent<Tile>().isWalkable || !telixSecond.GetComponent<Tile>().isWalkable)
        {
            return -1;
        }
        closeEnemies = (int)closeEnemies / 2;
        return closeEnemies;
    }

    public int distSmthToSmthTelix(int gX, int gY, int gX1, int gY1)
    {
        int distX = Mathf.Abs(gX - gX1);
        int distY = Mathf.Abs(gY - gY1);

        int distTo = (distX + distY);
        int distToPlant = distTo;
        //  Debug.Log(distToPlant + " " + who.name);

        return distX;
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

public class NodePos
{
    int y;
    int x;

    public NodePos(int nX, int nY)
    {
        x = nX;
        y = nY;
        
    }

}
