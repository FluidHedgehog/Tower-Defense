using UnityEngine;

public class InfoBookActions : MonoBehaviour
{
    public int currentPage;

    public bool isOpen = false;

    public GameObject[] pages;

    public void OpenFirstPage()
    {
        if (!isOpen) {
            currentPage = 0;
            pages[0].gameObject.SetActive(true);
            isOpen = true;
        } else
        {
            CloseBook();
        }
    }

    public void FlipPage(bool nextPage)
    {
        if (nextPage && currentPage < (pages.Length - 1))
        {
            pages[currentPage].gameObject.SetActive(false);
            currentPage += 1;
            pages[currentPage].gameObject.SetActive(true);
        }
        else if (!nextPage && currentPage > 0)
        {
            pages[currentPage].gameObject.SetActive(false);
            currentPage -= 1;
            pages[currentPage].gameObject.SetActive(true);
        }
    }

    public void CloseBook()
    {
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
        
        pages[0].SetActive(true);
        gameObject.SetActive(false);
        isOpen = false;
    }
}
