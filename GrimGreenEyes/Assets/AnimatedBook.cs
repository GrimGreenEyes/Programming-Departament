using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedBook : MonoBehaviour
{
    public List<AnimatedPage> pageList = new List<AnimatedPage>();
    private int pageIndex = 0;

    [SerializeField] private TextMeshProUGUI rightTitle, rightDescription, rightTitleAnim, rightDescriptionAnim;
    [SerializeField] private TextMeshProUGUI leftTitle, leftDescription, leftTitleAnim, leftDescriptionAnim;
    [SerializeField] private Animator pageAnimator;
    [SerializeField] private GameObject animationObject, rightObject, leftObject, rightObjectAnim, leftObjectAnim, bookPanel;
    [SerializeField] private Button nextPageR, prevPageR, closeBookR, nextPageL, prevPageL, closeBookL, nextPageRAnim, prevPageRAnim, closeBookRAnim, nextPageLAnim, prevPageLAnim, closeBookLAnim;
    [SerializeField] private GameObject bookmarks;
    [SerializeField] private Sprite normalBookmarkSprite, currentBookmarkSprite;
    [SerializeField] private Image rightImage, leftImage, rightImageAnim, leftImageAnim;
    private List<GameObject> bookmarksList = new List<GameObject>();
    public bool map = false;
    public bool combatScene = false;
    bool next;
    //[SerializeField] private UIManager uiManager;

    private void Start()
    {
        //CloseBook();
        //foreach (Transform child in bookmarks.transform)
        //{
        //    bookmarksList.Add(child.gameObject);
        //}
    }

    public void OpenBook()
    {
        rightImage.color = new Color(255f, 255f, 255f, 255f);
        foreach (Transform child in bookmarks.transform)
        {
            bookmarksList.Add(child.gameObject);
        }
        PlaySoundBook();
        bookPanel.SetActive(true);
        pageIndex = 0;

        rightTitle.text = pageList[pageIndex + 1].title;
        rightDescription.text = pageList[pageIndex + 1].description;
        rightImage.sprite = pageList[pageIndex + 1].image;

        leftTitle.text = pageList[pageIndex].title;
        leftDescription.text = pageList[pageIndex].description;
        leftImage.sprite = pageList[pageIndex].image;


        nextPageR.gameObject.SetActive(pageIndex <= pageList.Count - 3);
        closeBookR.gameObject.SetActive(pageIndex == pageList.Count - 2);

        prevPageL.gameObject.SetActive(pageIndex >= 2);
        closeBookL.gameObject.SetActive(pageIndex == pageList.Count - 1);

        ChooseBookmark();
    }

    public void PlaySoundBook()
    {
        GetComponent<AudioSource>().Play();
    }

    public GameObject canvas, player;
    public void CloseBook()
    {
        if (map)
        {
            canvas.SetActive(true);
            GameObject.Find("PLAYER").GetComponent<SpriteRenderer>().color += new Color(0f, 0f, 0f, 255f);
        }
        if (combatScene)
        {
            GameObject.Find("MainCamera").GetComponent<CameraController>().closedBook = true;
        }
        PlaySoundBook();
        bookPanel.SetActive(false);
    }

    public void NextPage()
    {
        PlaySoundBook();
        next = true;
        leftObjectAnim.SetActive(true);
        rightObjectAnim.SetActive(true);
        animationObject.SetActive(true);
        pageAnimator.SetBool("turnPage", true);

        //animationObject.SetActive(true);

        //rightObjectAnim.SetActive(true);
        //leftObjectAnim.SetActive(true);

        int pageIndexAux = pageIndex + 2;


        rightTitleAnim.text = pageList[pageIndex + 1].title;
        rightDescriptionAnim.text = pageList[pageIndex + 1].description;
        rightImageAnim.sprite = pageList[pageIndex + 1].image;

        leftTitleAnim.text = pageList[pageIndex + 2].title;
        leftDescriptionAnim.text = pageList[pageIndex + 2].description;
        leftImageAnim.sprite = pageList[pageIndex + 2].image;

        if(pageIndex + 3 < pageList.Count)
        {
            rightTitle.text = pageList[pageIndex + 3].title;
            rightDescription.text = pageList[pageIndex + 3].description;
            rightImage.sprite = pageList[pageIndex + 3].image;
        }
        else
        {
            rightTitle.text = "";
            rightDescription.text = "";
            rightImage.color = new Color(0f, 0f, 0f, 0f);
        }
        

        nextPageR.gameObject.SetActive(pageIndexAux <= pageList.Count - 3);
        closeBookR.gameObject.SetActive(pageIndexAux == pageList.Count - 2);

        prevPageLAnim.gameObject.SetActive(pageIndexAux >= 2);
        closeBookLAnim.gameObject.SetActive(pageIndexAux == pageList.Count - 1);

        nextPageRAnim.gameObject.SetActive(pageIndexAux <= pageList.Count - 3);
        closeBookRAnim.gameObject.SetActive(pageIndexAux == pageList.Count - 2);


    }

    public void PrevPage()
    {
        PlaySoundBook();
        next = false;
        leftObjectAnim.SetActive(true);
        rightObjectAnim.SetActive(true);
        animationObject.SetActive(true);
        pageAnimator.SetBool("prevPage", true);

        //animationObject.SetActive(true);

        //rightObjectAnim.SetActive(true);
        //leftObjectAnim.SetActive(true);

        int pageIndexAux = pageIndex - 2;


        //rightTitleAnim.text = pageList[pageIndex - 1].title;
        //rightDescriptionAnim.text = pageList[pageIndex - 1].description;

        leftTitleAnim.text = pageList[pageIndex - 2].title;
        leftDescriptionAnim.text = pageList[pageIndex - 2].description;
        leftImageAnim.sprite = pageList[pageIndex - 2].image;

        //rightTitle.text = pageList[pageIndex - 1].title;
        //rightDescription.text = pageList[pageIndex - 1].description;

        //nextPageR.gameObject.SetActive(pageIndexAux <= pageList.Count - 3);
        //closeBookR.gameObject.SetActive(pageIndexAux == pageList.Count - 2);

        prevPageLAnim.gameObject.SetActive(pageIndexAux >= 2);
        closeBookLAnim.gameObject.SetActive(pageIndexAux == pageList.Count - 1);

        //nextPageRAnim.gameObject.SetActive(pageIndexAux <= pageList.Count - 3);
        //closeBookRAnim.gameObject.SetActive(pageIndexAux == pageList.Count - 2);
    }

    public void EndAnimation()
    {
        pageAnimator.SetBool("turnPage", false);

        pageAnimator.SetBool("prevPage", false);
        //animationObject.SetActive(false);
        //rightObjectAnim.SetActive(false);
        //leftObjectAnim.SetActive(false);

        if (next) pageIndex += 2;
        else pageIndex -= 2;

        leftObjectAnim.SetActive(false);
        rightObjectAnim.SetActive(false);
        animationObject.SetActive(false);

        ChooseBookmark();
    }

    public void WriteLeft()
    {
        int pageIndexAux;
        if (next) pageIndexAux = pageIndex + 2;
        else pageIndexAux = pageIndex - 2;

        if (next)
        {
            leftTitle.text = pageList[pageIndex + 2].title;
            leftDescription.text = pageList[pageIndex + 2].description;
            leftImage.sprite = pageList[pageIndex + 2].image;
        }
        else
        {
            leftTitle.text = pageList[pageIndex - 2].title;
            leftDescription.text = pageList[pageIndex - 2].description;
            leftImage.sprite = pageList[pageIndex - 2].image;
        }
        

        prevPageL.gameObject.SetActive(pageIndexAux >= 2);
        closeBookL.gameObject.SetActive(pageIndexAux == pageList.Count - 1);
    }

    public void WriteRight()
    {
        rightImage.color = new Color(255f, 255f, 255f, 255f);
        int pageIndexAux = pageIndex - 2;
        rightTitleAnim.text = pageList[pageIndex - 1].title;
        rightDescriptionAnim.text = pageList[pageIndex - 1].description;
        rightImageAnim.sprite = pageList[pageIndex - 1].image;

        rightTitle.text = pageList[pageIndex - 1].title;
        rightDescription.text = pageList[pageIndex - 1].description;
        rightImage.sprite = pageList[pageIndex - 1].image;

        nextPageR.gameObject.SetActive(pageIndexAux <= pageList.Count - 3);
        closeBookR.gameObject.SetActive(pageIndexAux == pageList.Count - 2);

        nextPageRAnim.gameObject.SetActive(pageIndexAux <= pageList.Count - 3);
        closeBookRAnim.gameObject.SetActive(pageIndexAux == pageList.Count - 2);
    }

    private void ChooseBookmark()
    {
        foreach(GameObject bookmark in bookmarksList)
        {
            bookmark.GetComponent<Image>().sprite = normalBookmarkSprite;
        }

        int currentBookmark = pageIndex / 2;

        if(currentBookmark >= bookmarksList.Count)
        {
            bookmarksList[bookmarksList.Count-1].GetComponent<Image>().sprite = currentBookmarkSprite;
        }
        else
        {
            bookmarksList[currentBookmark].GetComponent<Image>().sprite = currentBookmarkSprite;
        }

        foreach (GameObject bookmark in bookmarksList)
        {
            bookmark.GetComponent<Image>().SetNativeSize();
        }
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
