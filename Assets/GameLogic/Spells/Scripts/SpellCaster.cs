using UnityEngine;

public static class SpellCasterEvents
{
    public static event System.Action<Vector2> OnTransformSpell;
    public static event System.Action OnCastSpell;

    public static void TriggerTransformSpell(Vector2 vector) => OnTransformSpell?.Invoke(vector);
    public static void TriggerCastSpell() => OnCastSpell?.Invoke();

}

public class SpellCaster : MonoBehaviour
{
    [SerializeField] ManaSystem manaSystem;

    GameObject currentSpell;

    void OnEnable()
    {
        SpellCasterEvents.OnTransformSpell += TransformSpell;
        SpellCasterEvents.OnCastSpell += CastSpell;
    }

    void OnDisable()
    {
        SpellCasterEvents.OnTransformSpell -= TransformSpell;
        SpellCasterEvents.OnCastSpell -= CastSpell;
    }

    public void InitializeSpell(GameObject spellPrefab)
    {

        if (spellPrefab == null)
        {
            Debug.LogWarning("No SpellPrefab!");
        }
        if (spellPrefab.GetComponent<SpellInstance>() == null)
        {
            Debug.LogWarning("No SpellInstance!");
        }
        if (!manaSystem.CanSpell(spellPrefab.GetComponent<SpellInstance>().cost))
        {
            return;
        }
        currentSpell = Instantiate(spellPrefab, transform.position, Quaternion.identity);
        ChangeStates.ChangeStateNow(3);
    }

    void TransformSpell(Vector2 pos)
    {
        currentSpell.transform.position = pos;
    }

    void CastSpell()
    {
        currentSpell.GetComponent<SpellInstance>().TriggetEffect();
        ManaSystemEvents.TriggerManaRemoved(currentSpell.GetComponent<SpellInstance>().cost);
        Destroy(currentSpell);
    }

}
