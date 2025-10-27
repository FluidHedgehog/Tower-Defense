using UnityEngine;
using UnityEngine.EventSystems;

public class SpellMenuActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject spellMenu;
    private bool isHoveringMenu = false;
    private bool isHoveringButton = false;
    private float hideTimer = 0f;
    private float hideDelay = 0.15f;

    void Update()
    {
        if (spellMenu != null && spellMenu.activeSelf && !isHoveringButton && !isHoveringMenu)
        {
            hideTimer += Time.deltaTime;
            if (hideTimer >= hideDelay)
            {
                spellMenu.SetActive(false);
                hideTimer = 0f;
            }
        }
        else
        {
            hideTimer = 0f;
        }

        Debug.Log(hideTimer);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoveringButton = true;
        if (spellMenu != null)
            spellMenu.SetActive(true);
        hideTimer = 0f;
    }

    public void OnPointerExit(PointerEventData eventData) { isHoveringButton = false; }

    public void SetHoveringMenu(bool state)
    {
        isHoveringMenu = state;
        if (state && spellMenu != null)
        {
            spellMenu.SetActive(true);
            hideTimer = 0f;
        }
    }
}
