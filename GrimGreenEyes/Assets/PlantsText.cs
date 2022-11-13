using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlantsText : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public List<Info> infos = new List<Info>();

    public GameObject blcImg;
    public GameObject text;
    public GameObject plantImg;
    public GameObject plantName;

    void Start()
    {
        GameObject.Find("Continue").GetComponent<Button>().interactable = false;

        blcImg = transform.GetChild(0).gameObject;
        text = blcImg.transform.GetChild(0).gameObject;
        plantImg = blcImg.transform.GetChild(1).gameObject;
        plantName = blcImg.transform.GetChild(2).gameObject;



        int rand = Random.RandomRange(0, infos.Count);
        Info info = infos[rand];

        text.GetComponent<TextMeshProUGUI>().text = info.msgEng;
        plantImg.GetComponent<Image>().sprite = info.image;
        plantName.GetComponent<TextMeshProUGUI>().text = info.name;
        StartCoroutine(loadingScene());

    }
    private void OnEnable()
    {
        GameObject.Find("Continue").GetComponent<Button>().interactable = false;

        int rand = Random.RandomRange(0, infos.Count);
        Info info = infos[rand];

        text.GetComponent<TextMeshProUGUI>().text = info.msgEng;
        plantImg.GetComponent<Image>().sprite = info.image;
        plantName.GetComponent<TextMeshProUGUI>().text = info.name;

        StartCoroutine(loadingScene());

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


    IEnumerator loadingScene()
    {
        int rand = Random.RandomRange(2, 10);
        yield return new WaitForSeconds(rand);
        GameObject.Find("Continue").GetComponent<Button>().interactable = true;
    }
}
[System.Serializable]
public class Info{
    public string name;
    public Sprite image;

    public string msg;
    public string msgEng;

}
