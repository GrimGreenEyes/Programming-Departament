using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    [SerializeField] private GlobalVar globalVar;
    [SerializeField] private AnimatedBook book;
    [SerializeField] private GameObject canvas;
    bool first = true;

    private void Start()
    {
        globalVar = GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>();
        if (globalVar.firstTimeMap)
        {
            
            book.OpenBook();
            book.map = true;
            //GameObject.Find("CanvasNodes").SetActive(true);
        }
    }

    private void Update()
    {

        if (canvas == null)
        {
            try
            {
                canvas = GameObject.Find("CanvasNodes").gameObject;
            }
            catch { }
        }

        if (first && globalVar.firstTimeMap)
        {
            try
            {

                canvas.SetActive(false);
                globalVar.firstTimeMap = false;
                book.player = GameObject.Find("PLAYER");
                GameObject.Find("PLAYER").GetComponent<SpriteRenderer>().color -= new Color(0f, 0f, 0f, 255f);
                first = false;
                book.canvas = canvas;
            }
            catch { }
            
        }
    }
}
