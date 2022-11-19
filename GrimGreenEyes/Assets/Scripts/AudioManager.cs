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

    public Songs pushB;

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
        
       // StartCoroutine(WaitForSound(music));
    }
    
    public void clickAndChangeMusic(string music)
    {
        StartCoroutine(WaitForSound(music));
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

    public IEnumerator WaitForSound(string music)
    {
        audioSource.loop = false;
        actualClip = pushB.clip;
        audioSource.clip = actualClip;
        audioSource.Play();
        Debug.Log(pushB.name);
        yield return new WaitUntil(() => audioSource.isPlaying == false);

        audioSource.loop = true;
        actualClip = sources[audioIndex(music)].clip;
        audioSource.clip = actualClip;
        audioSource.Play();
        if (music == "llanura1")
            StartCoroutine(changeLlanuraSong());
        else if (music == "jungla1")
            StartCoroutine(changeJunglaSong());
    }

    public IEnumerator JustClick()
    {
        Debug.Log("JustClick");
        float songTime = audioSource.time;
        audioSource.loop = false;
        actualClip = pushB.clip;
        audioSource.clip = actualClip;
        audioSource.Play();
        yield return new WaitUntil(() => audioSource.isPlaying == false);

        audioSource.loop = true;
        actualClip = sources[1].clip;
     //   audioSource.time = songTime;
        audioSource.Play();
    }
}




[System.Serializable]
public class Songs
{
   public string name;
   public AudioClip clip;
}
