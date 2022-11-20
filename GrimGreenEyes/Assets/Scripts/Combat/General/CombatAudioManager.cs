using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> desertMusic = new List<AudioClip>();
    [SerializeField] private List<AudioClip> jungleMusic = new List<AudioClip>();
    [SerializeField] private List<AudioClip> plainMusic = new List<AudioClip>();
    [SerializeField] private List<AudioClip> snowMusic = new List<AudioClip>();
    private List<AudioClip> music= new List<AudioClip>();
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Init(EnumMapOptions.mapBiom biome)
    {
        switch (biome)
        {
            case EnumMapOptions.mapBiom.desierto:
                music = desertMusic;
                break;
            case EnumMapOptions.mapBiom.llanura:
                music = desertMusic;
                break;
            case EnumMapOptions.mapBiom.selva:
                music = desertMusic;
                break;
            case EnumMapOptions.mapBiom.nieve:
                music = desertMusic;
                break;
        }
        audioSource.clip = music[0];
        if(music.Count == 1)
        {
            audioSource.loop = true;
        }
    }
    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = music[1];
            audioSource.Play();
            audioSource.loop = true;
        }
    }
}
