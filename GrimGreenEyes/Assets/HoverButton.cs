using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float offsetX, offsetY;
    [SerializeField] private Sprite normalSprite, hoverSprite;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GetComponent<Button>().interactable) return;
        GetComponent<Image>().sprite = hoverSprite;
        GetComponent<Image>().SetNativeSize();
        transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!GetComponent<Button>().interactable) return;
        GetComponent<Image>().sprite = normalSprite;
        GetComponent<Image>().SetNativeSize();
        transform.position = new Vector3(transform.position.x - offsetX, transform.position.y - offsetY, transform.position.z);
    }
}
