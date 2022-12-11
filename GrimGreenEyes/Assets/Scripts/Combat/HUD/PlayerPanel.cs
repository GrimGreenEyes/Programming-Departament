using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerPanel : MonoBehaviour
{
    public static PlayerPanel instance;
    private GameObject startPanel;

    [SerializeField] private Sprite[] plants;
    [SerializeField] private GameObject buttonPref;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text livePointsText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Transform[] butonPositions = new Transform[3];
    [SerializeField] private TMP_Text PS;
    [SerializeField] private TMP_Text Att;
    [SerializeField] private TMP_Text Def;
    [SerializeField] private TMP_Text Agl;
    [SerializeField] private TMP_Text Hres;
    [SerializeField] private TMP_Text CRes;
    [SerializeField] private TMP_Text steps;

    public Entity plant; 

    private List<GameObject> skillButtons = new List<GameObject>();

    public GameObject attackBtn;
    public GameObject moveBtn;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        startPanel = GameObject.Find("GlobalAttributes").transform.GetChild(0).gameObject;
    }

    private void OnDestroy()
    {
        if(instance = this)
        {
            instance = null;
        }
    }
    private void Update()
    {
        if (startPanel.activeSelf)
        {
            return;
        }
        Att.text = plant.attack.ToString();
        Def.text = plant.defense.ToString();
        Agl.text = plant.agility.ToString();
        Hres.text = plant.heatResistance.ToString();
        CRes.text = plant.freezeResistance.ToString();
        steps.text = plant.movement.ToString();
        PS.text = plant.livePoints.ToString() + " / " + plant.maxLivePoints.ToString();
        
        if(plant.movement == 0)
        {
            moveBtn.GetComponent<Button>().interactable = false;
        }
        if (plant.attacked)
        {
            attackBtn.GetComponent<Button>().interactable = false;
        }
    }
    public void ChangePlayer(GameObject newPlant)
    {
        plant = newPlant.GetComponent<Entity>();
        moveBtn.GetComponent<Button>().interactable = true;
        attackBtn.GetComponent<Button>().interactable = true;

        image.sprite = plant.HUDSprite;
        image.type = Image.Type.Tiled;
        image.pixelsPerUnitMultiplier = plant.HUDSpriteSize;
        image.preserveAspect = true;
        nameText.SetText(plant.name);
        skillButtons.Clear();
        livePointsText.SetText(plant.livePoints.ToString() + " / " + plant.maxLivePoints.ToString());
        int x = 0;
        for (int i = 0; i < butonPositions.Length; i++)
        {
            if (butonPositions[i].childCount > 0)
            {
                Destroy(butonPositions[i].GetChild(0).gameObject);
            }
        }
        for (int i = 0; i < plant.skills.Count; i++)
        {
            if(plant.skills[i] == null)
            {
                return;
            }
            
            {
                GameObject button = Instantiate(buttonPref, butonPositions[x], false);
                skillButtons.Add(button);
                button.GetComponent<ButtonManager>().Init(i);
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = plant.skills[i].name;
                x++;
            }
        }
    }
    public void SetButtonsToInactive()
    {
        for (int i = 0; i < skillButtons.Count; i++)
        {
            if(GameController.instance.SelectedPlayer().GetComponent<Entity>().skills[i].currentCoolDown == 0)
            {
                skillButtons[i].GetComponent<ButtonManager>().isActive = false;
                skillButtons[i].GetComponent<ButtonManager>().SetColor(skillButtons[i].GetComponent<ButtonManager>().onButtonUpColor);
                GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState = Entity.EntityState.IDLE;
            }
        }
    }
    public void SetAttack()
    {
        if(GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState != Entity.EntityState.IDLE && GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState != Entity.EntityState.READYTOMOVE && GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState != Entity.EntityState.READYTOATTACK)
        {
            return;
        }
        GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState = (GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState == Entity.EntityState.IDLE || GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState == Entity.EntityState.READYTOMOVE) ?  Entity.EntityState.READYTOATTACK : Entity.EntityState.IDLE;
    }
    public void SetMovement()
    {
        if (GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState != Entity.EntityState.IDLE && GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState != Entity.EntityState.READYTOMOVE && GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState != Entity.EntityState.READYTOATTACK)
        {
            return;
        }
        GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState = (GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState == Entity.EntityState.IDLE || GameController.instance.SelectedPlayer().GetComponent<Entity>().actualState == Entity.EntityState.READYTOATTACK) ? Entity.EntityState.READYTOMOVE : Entity.EntityState.IDLE;
    }
}
