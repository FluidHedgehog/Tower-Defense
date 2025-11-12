using UnityEngine;

public class InfoBookActions : MonoBehaviour
{
    public int currentPage;

    public GameObject[] pages;

    public void OpenFirstPage()
    {
        currentPage = 0;
        pages[0].gameObject.SetActive(true);
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

    public void CloseIfTutorialEnded()
    {
        if(currentPage == pages.Length -1 )
        {
            CloseBook();
        }
    }

    public void CloseBook()
    {
        gameObject.SetActive(false);
    }
}
