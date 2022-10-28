using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : MonoBehaviour
{
    public int waterAmount;
    [SerializeField] private int maxWater = 12;
    private float sizeOfTank;
    [SerializeField] private GameObject waterObject;
    public float waterDistance;

    private void Start()
    {
        sizeOfTank = GetComponent<RectTransform>().sizeDelta.y;
        UpdateWater();
    }

    private void UpdateWater()
    {
        waterDistance = sizeOfTank / maxWater;
        waterObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, - (maxWater - waterAmount) * waterDistance, 0f);
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
