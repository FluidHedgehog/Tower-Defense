using System.Collections.Generic;
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

    public GameObject currentSpell;
    public GameObject currentSpellAnim;

    Dictionary<SpellType, float> spellCooldowns = new();

    SpellType currentSpellType;

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

    void Update()
    {
        List<SpellType> keys = new(spellCooldowns.Keys);
        foreach (var key in keys)
        {
            if (spellCooldowns[key] > 0)
            {
                spellCooldowns[key] -= Time.deltaTime;
            }
        }
    }

    public void InitializeSpell(GameObject spellPrefab)
    {
        if (spellPrefab == null)
        {
            Debug.LogWarning("No SpellPrefab!");
        }

        SpellInstance instance = spellPrefab.GetComponent<SpellInstance>();

        if (spellPrefab.GetComponent<SpellInstance>() == null)
        {
            Debug.LogWarning("No SpellInstance!");
        }

        currentSpellType = instance.spellType;

        float remainingCooldown = spellCooldowns.ContainsKey(currentSpellType) ? spellCooldowns[currentSpellType] : 0;

        if (remainingCooldown > 0)
        {
            Debug.LogWarning("Spell on cooldown!");
            return;
        }

        if (!manaSystem.CanSpell(instance.spell.cost))
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
        PlaySpellAnimation();
        SpellInstance spellInstance = currentSpell.GetComponent<SpellInstance>();
        spellInstance.TriggetEffect();
        ManaSystemEvents.TriggerManaRemoved(spellInstance.cost);

        spellCooldowns[spellInstance.spellType] = spellInstance.spell.cooldown;

        Destroy(currentSpell);
        currentSpell = null;
    }

    public void AssignSpellAnimation(GameObject spellAnim)
    {
        currentSpellAnim = spellAnim;
    }

    void PlaySpellAnimation()
    {
        Instantiate(currentSpellAnim, (Vector3)currentSpell.transform.position, Quaternion.identity);
    }

    public void DestroySpellAnimation()
    {
        Destroy(currentSpellAnim);
        currentSpellAnim = null;
    }

    public Dictionary<SpellType, float> GetCooldownInfo()
    {
        return new Dictionary<SpellType, float>(spellCooldowns);
    }
}
