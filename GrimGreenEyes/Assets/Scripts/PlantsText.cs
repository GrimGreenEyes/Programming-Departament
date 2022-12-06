using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class PlantsText : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public List<Info> infos = new List<Info>();

    public GameObject blcImg;
    public GameObject text;
    public GameObject plantImg;
    public GameObject plantName;

    public GameObject cont;

    void Start()
    {
        try
        {
            GameObject.Find("Continue").GetComponent<Button>().interactable = false;
        }
        catch
        {
            cont.GetComponent<Button>().interactable = false;
        }

        blcImg = transform.GetChild(0).gameObject;
        text = blcImg.transform.GetChild(0).gameObject;
        plantImg = blcImg.transform.GetChild(1).gameObject;
        plantName = blcImg.transform.GetChild(2).gameObject;



        int rand = Random.RandomRange(0, infos.Count);
        Info info = infos[rand];

        text.GetComponent<TextMeshProUGUI>().text = info.msg;
        plantImg.GetComponent<Image>().sprite = info.image;
        plantName.GetComponent<TextMeshProUGUI>().text = info.name;

        StartCoroutine(loadingScene(1,4));

    }
    private void OnEnable()
    {
        try
        {
            GameObject.Find("Continue").GetComponent<Button>().interactable = false;
        }
        catch { cont.GetComponent<Button>().interactable = false; }

            blcImg = transform.GetChild(0).gameObject;
            text = blcImg.transform.GetChild(0).gameObject;
            plantImg = blcImg.transform.GetChild(1).gameObject;
            plantName = blcImg.transform.GetChild(2).gameObject;


            int rand = Random.RandomRange(0, infos.Count);
            Info info = infos[rand];

            text.GetComponent<TextMeshProUGUI>().text = info.msg;
            plantImg.GetComponent<Image>().sprite = info.image;
            plantName.GetComponent<TextMeshProUGUI>().text = info.name;
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "ResourcesScene" || scene.name == "MapScene")
        {
            Debug.Log("Resources or map");
            Debug.Log("SHORT LOAD");

            StartCoroutine(loadingScene(0, 2));
        }
        else
        {
            Debug.Log(scene.name);
            Debug.Log("LONG LOAD");

            StartCoroutine(loadingScene(2, 6));

        }

    }

    /*public void updatePlant()
    {
            GameObject.Find("Continue").GetComponent<Button>().interactable = false;

        int rand = Random.RandomRange(0, infos.Count);
        Info info = infos[rand];

        text.GetComponent<TextMeshProUGUI>().text = info.msgEng;
        plantImg.GetComponent<Image>().sprite = info.image;
        plantName.GetComponent<TextMeshProUGUI>().text = info.name;

            StartCoroutine(loadingScene());


    }*/

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator loadingScene(int min, int max)
    {
        int rand = 0;
        try
        {
            GameObject.Find("Continue").GetComponent<Button>().gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Loading...";

            rand = Random.RandomRange(min, max);
        }
        catch
        {
            cont.GetComponent<Button>().gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Loading...";

            rand = Random.RandomRange(min, max);
        }
        yield return new WaitForSeconds(rand);
        try
        {
            GameObject.Find("Continue").GetComponent<Button>().interactable = true;

            GameObject.Find("Continue").GetComponent<Button>().gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PLAY";
        }
        catch
        {
            cont.GetComponent<Button>().interactable = true;

            cont.GetComponent<Button>().gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PLAY";
        }
    }
}
[System.Serializable]
public class Info{
    public string name;
    public Sprite image;

    public string msg;
    public string msgEng;

}
