using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<Button> sceneElements = new List<Button>();
    public PlantDetailsDisplay plantDetailsDisplay;
    public GameObject warningMsgPrefab;
    private GameObject currentWarning = null;
    public FollowMouse followMouse;
    public GameObject exitButton;

    public AudioClip useTap, waterDrop, plantSeed, bookPage, useBlender;
    public AudioSource audioSource, audioSourceAux;

    private void Start()
    {
        
    }

    public void PlaySoundTap()
    {
        audioSource.clip = useTap;
        audioSource.Play();
    }

    public void PlaySoundBlender()
    {
        audioSource.clip = useBlender;
        audioSource.Play();
    }

    public void PlaySoundWater()
    {
        audioSourceAux.clip = waterDrop;
        audioSourceAux.Play();
    }

    public void PlaySoundSeed()
    {
        audioSource.clip = plantSeed;
        audioSource.Play();
    }

    public void PlaySoundBook()
    {
        audioSource.clip = bookPage;
        audioSource.Play();
    }

    public void HideExitButton()
    {
        exitButton.SetActive(false);
    }

    public void ShowExitButton()
    {
        exitButton.SetActive(true);
    }

    public void DesactivateAllElements()
    {
        foreach(Button button in sceneElements)
        {
            button.interactable = false;
        }
    }

    public void ActivateAllElements()
    {
        foreach (Button button in sceneElements)
        {
            button.interactable = true;
        }
    }

    public void AddButton(Button button)
    {
        sceneElements.Add(button);
    }

    public void RemoveButton(Button button)
    {
        sceneElements.Remove(button);
    }

    public void ShowWarning(string msg, float time)
    {
        WarningMsg warning = Instantiate(warningMsgPrefab).GetComponent<WarningMsg>();
        Destroy(currentWarning);
        currentWarning = warning.gameObject;
        warning.DisplayMsg(msg, time);
    }
}
