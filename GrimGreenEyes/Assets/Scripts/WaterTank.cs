using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : MonoBehaviour
{
    public int waterAmount;
    [SerializeField] public int maxWater = 13;
    private float sizeOfTank;
    [SerializeField] private GameObject waterObject;
    public float waterDistance;

    public float startingY = -83;
    public float gapY = 30;

    bool first = true;

    private void Start()
    {
        sizeOfTank = GetComponent<RectTransform>().sizeDelta.y;
        UpdateWater();
    }

    public void UpdateWater()
    {
        if (first)
        {
            waterObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, startingY - (gapY * (maxWater - waterAmount)), 0f);
            first = false;
        }
        StopAllCoroutines();
        StartCoroutine(MoveWaterInTime(waterObject.GetComponent<RectTransform>().anchoredPosition.y, startingY - (gapY * (maxWater - waterAmount)), 1));
        //waterObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, startingY - (gapY * (maxWater - waterAmount)), 0f);
        
        //waterDistance = sizeOfTank / maxWater;
        //waterObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, - (maxWater - waterAmount) * waterDistance, 0f);
    }

    IEnumerator MoveWaterInTime(float origin, float destiny, float time)
    {
        float distance = destiny - origin;
        int frames = 20;

        float distPerFrame = distance / frames;
        float timePerFrame = time / frames;

        for(int i = 0; i < frames; i++)
        {
            waterObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, waterObject.GetComponent<RectTransform>().anchoredPosition.y + distPerFrame, 0f);
            yield return new WaitForSeconds(timePerFrame);
        }
    }

    public void AddWater()
    {
        if (waterAmount >= maxWater) return;

        waterAmount++;
        UpdateWater();
    }

    public void RemoveWater()
    {
        if (waterAmount <= 0) return;

        waterAmount--;
        UpdateWater();
    }

    public void SetWaterAmount(int cant)
    {
        if(cant >= maxWater)
        {
            waterAmount = maxWater;
        }
        else
        {
            waterAmount = cant;
        }
    }
}
