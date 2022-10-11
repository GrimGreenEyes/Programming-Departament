using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerManager : MonoBehaviour
{
    private Dictionary<Fertilizer, int> storedFertilizers = new Dictionary<Fertilizer, int>(); //Almacena los fertilizantes y su cantidad
    [SerializeField] private List<InventorySlot> slotsList = new List<InventorySlot>(); //Almacena los SLOTS (objetos con botones) del inventario
    [SerializeField] private GameObject fertilizersPanel;

    private void Start()
    {
        foreach (Transform child in fertilizersPanel.transform)
        {
            slotsList.Add(child.GetComponent<InventorySlot>());
        }
        UpdateFertilizers();
    }

    public void AddFertilizer(Fertilizer fertilizer) //Añade un fertilizante al diccionario (inventario). Si ese fertilizante ya existía, aumenta su cantidad en 1 unidad.
    {
        if (storedFertilizers.ContainsKey(fertilizer))
        {
            storedFertilizers[fertilizer] = storedFertilizers[fertilizer] + 1;
        }
        else
        {
            storedFertilizers.Add(fertilizer, 1);
        }
        UpdateFertilizers();
    }

    public void RemoveFertilizer(Fertilizer fertilizer) //Reduce en 1 unidad la cantidad del fertilizante indicado. Si la cantidad llega a 0, el fertilizante es borrado de la lista.
    {
        if (!storedFertilizers.ContainsKey(fertilizer))
        {
            Debug.Log("THE ITEM " + fertilizer.name + " DOES NOT EXIST IN THE INVENTORY");
            return;
        }

        storedFertilizers[fertilizer] = storedFertilizers[fertilizer] - 1;
        if (storedFertilizers[fertilizer] <= 0)
        {
            storedFertilizers.Remove(fertilizer);
        }
        UpdateFertilizers();
    }

    public void UpdateFertilizers()
    {
        foreach (InventorySlot slot in slotsList)
        {
            slot.ResetSlot();
        }

        int i = 0;
        foreach (KeyValuePair<Fertilizer, int> entry in storedFertilizers)
        {
            slotsList[i].SetFertilizerAndAmount(entry.Key, entry.Value);
            i++;
        }
    }
}
