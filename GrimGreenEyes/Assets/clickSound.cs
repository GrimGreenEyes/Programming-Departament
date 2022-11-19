using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickSound : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip clickSong;
    public AudioSource buttonClick;
    void Start()
    {
        
    }

    public void SoundClick()
    {
        buttonClick.clip = clickSong;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
