using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public List<Songs> sources = new List<Songs>(); 
    public AudioClip actualClip; 
    public AudioSource audioSource;

   /* public enum audioOptions
    {
        intro,
        menu,
        mapa, 
        carro
    }*/
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeMusic(string music)
    {
        actualClip = sources[audioIndex(music)].clip;
        audioSource.clip = actualClip;
       audioSource.Play();
        if (music == "llanura1")
            StartCoroutine(changeLlanuraSong());
        else if (music == "jungla1")
            StartCoroutine(changeJunglaSong());

    }

    private int audioIndex(string song)
    {
        for (int i = 0; i < sources.Count; i++)
        {
            if (sources[i].name == song)
                return i;
        }
        return 0;
    }

    IEnumerator changeLlanuraSong()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        changeMusic("llanura2");
    }
    IEnumerator changeJunglaSong()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        changeMusic("jungla2");
    }
}




[System.Serializable]
public class Songs
{
   public string name;
   public AudioClip clip;


}
