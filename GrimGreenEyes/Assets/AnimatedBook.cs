using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimatedBook : MonoBehaviour
{
    public List<AnimatedPage> pageList = new List<AnimatedPage>();
    private int pageIndex = 0;

    [SerializeField] private TextMeshProUGUI rightTitle, rightDescription, rightTitleAnim, rightDescriptionAnim;
    [SerializeField] private TextMeshProUGUI leftTitle, leftDescription, leftTitleAnim, leftDescriptionAnim;
    [SerializeField] private Animator pageAnimator;
    [SerializeField] private GameObject animationObject, rightObject, leftObject, rightObjectAnim, leftObjectAnim;

    private void Start()
    {
        OpenBook();
    }

    public void OpenBook()
    {
        rightTitle.text = pageList[pageIndex + 1].title;
        rightDescription.text = pageList[pageIndex + 1].description;
        leftTitle.text = pageList[pageIndex].title;
        leftDescription.text = pageList[pageIndex].description;
    }

    public void CloseBook()
    {

    }

    public void NextPage()
    {
        pageAnimator.SetBool("turnPage", true);

        //animationObject.SetActive(true);

        //rightObjectAnim.SetActive(true);
        //leftObjectAnim.SetActive(true);


        rightTitleAnim.text = pageList[pageIndex + 1].title;
        rightDescriptionAnim.text = pageList[pageIndex + 1].description;

        leftTitleAnim.text = pageList[pageIndex + 2].title;
        leftDescriptionAnim.text = pageList[pageIndex + 2].description;

        rightTitle.text = pageList[pageIndex + 3].title;
        rightDescription.text = pageList[pageIndex + 3].description;
    }

    public void EndAnimation()
    {
        pageAnimator.SetBool("turnPage", false);
        //animationObject.SetActive(false);
        //rightObjectAnim.SetActive(false);
        //leftObjectAnim.SetActive(false);

        pageIndex += 2;
        
    }

    public void WriteLeft()
    {
        leftTitle.text = pageList[pageIndex + 2].title;
        leftDescription.text = pageList[pageIndex + 2].description;
    }



}

[System.Serializable]
public class AnimatedPage
{
    public string title;
    [TextArea(3, 6)]
    public string description;
    public Sprite image;
}
