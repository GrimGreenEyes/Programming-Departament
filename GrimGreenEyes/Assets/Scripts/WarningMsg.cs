using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningMsg : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI warningText;
    public Animator anim;
    private GameObject parent;

    private void Start()
    {
        parent = GameObject.Find("Canvas");
        transform.SetParent(parent.transform);
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.localPosition = new Vector3(0f, -155f, 0);
    }

    public void Disappear()
    {
        Destroy(this.gameObject);
    }

    public void DisplayMsg(string text, float time)
    {
        anim = GetComponent<Animator>();
        warningText.text = text;

        StartCoroutine(PrintForSeconds(time));
    }

    IEnumerator PrintForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        anim.SetBool("displayed", true);
    }
}
