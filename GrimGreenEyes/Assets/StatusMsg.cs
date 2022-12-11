using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusMsg : MonoBehaviour
{
    //public Animator anim;
    //private GameObject parent;

    //public void Disappear()
    //{
    //    Destroy(this.gameObject);
    //}

    IEnumerator Disappear()
    {
        for(int i = 0; i < 30; i++)
        {
            this.transform.position = this.transform.position + new Vector3(0f, 0.01f, 0f);
            GetComponent<TextMeshProUGUI>().color = GetComponent<TextMeshProUGUI>().color - new Color(0f, 0f, 0f, 0.03f);

            yield return new WaitForSeconds(0.01f);
        }
        Destroy(this.gameObject);
    }

    public void DisplayMsg(GameObject parent, string text, Color color, float duration)
    {
        transform.SetParent(parent.transform);
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.localPosition = new Vector3(0f, 0f, 0f);

        //anim = GetComponent<Animator>();
        GetComponent<TextMeshProUGUI>().text = text;
        GetComponent<TextMeshProUGUI>().color = color;

        StartCoroutine(PrintForSeconds(duration));
    }

    IEnumerator PrintForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        //anim.SetBool("displayed", true);
        StartCoroutine(Disappear());
    }
}
