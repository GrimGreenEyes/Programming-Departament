using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPotCollider : MonoBehaviour
{
    [SerializeField] private FlowerPot referencedPot;

    private void Start()
    {
        referencedPot.potCollider = this;
        gameObject.SetActive(false);
    }

    public void ChooseReferencedPot()
    {
        referencedPot.ChoosePot();
    }
}
