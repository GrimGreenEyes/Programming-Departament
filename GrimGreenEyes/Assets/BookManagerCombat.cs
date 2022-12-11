using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManagerCombat : MonoBehaviour
{
    GlobalVar globalVar;
    [SerializeField] private AnimatedBook book;

    private void Start()
    {
        globalVar = GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>();
        if (globalVar.firstTimeCombat)
        {
            book.OpenBook();
            book.combatScene = true;
            globalVar.firstTimeCombat = false;
        }
    }
}
