using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] private List<Sprite> statusImages;
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    public void SetImage(int num)
    {
        renderer.sprite = statusImages[num];
    }

}
