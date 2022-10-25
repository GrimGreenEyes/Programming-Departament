using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIHoverListener : MonoBehaviour
{
    public static UIHoverListener instance;
    public bool isUIOverride { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    void Update()
    {
        // It will turn true if hovering any UI Elements
        isUIOverride = EventSystem.current.IsPointerOverGameObject();
    }
}
