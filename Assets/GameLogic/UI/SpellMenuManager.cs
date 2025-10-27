using UnityEngine;
using UnityEngine.EventSystems;

public class SpellMenuManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SpellMenuActivator activator;

    public void OnPointerEnter(PointerEventData eventData) { activator.SetHoveringMenu(true); }

    public void OnPointerExit(PointerEventData eventData) { activator.SetHoveringMenu(false); }
}
