using UnityEngine;
using UnityEngine.InputSystem;

public static class SpellStateEvents
{
    
    public static void OnPoint(Vector2 mousePos)
    {
        SpellCasterEvents.TriggerTransformSpell(mousePos);
    }

    public static void OnInteract()
    {
        SpellCasterEvents.TriggerCastSpell();
        ChangeStates.ChangeStateNow(0);
    }

    public static void OnHold(Vector2 mousePos)
    {

    }

    public static void OnRelease(Vector2 mousePos)
    {

    }
}
