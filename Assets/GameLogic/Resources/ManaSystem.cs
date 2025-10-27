using UnityEngine;
using UnityEngine.UI;

public static class ManaSystemEvents
{   
    public static event System.Action<int> OnManaAdded;
    public static event System.Action<int> OnManaRemoved;
    public static event System.Func<int, bool> OnPassValue;

    public static void TriggerManaAdded(int val) => OnManaAdded?.Invoke(val);
    public static void TriggerManaRemoved(int val) => OnManaRemoved?.Invoke(val);
    public static bool TriggerPassValue(int val) => OnPassValue?.Invoke(val) ?? false;
}

public class ManaSystem : MonoBehaviour
{
    [SerializeField] Slider manaSlider;

    [SerializeField] int maxMana;

    [SerializeField] int manaAddValue;

    [SerializeField] float manaAddCooldown;
    [SerializeField] float manaAddTimer;

    public int currentMana { get; set; }

    void ChangeValue()
    {
        manaSlider.value = currentMana;
    }

    void OnEnable()
    {
        ManaSystemEvents.OnManaAdded += OnManaAdded;
        ManaSystemEvents.OnManaRemoved += OnManaRemoved;
        ManaSystemEvents.OnPassValue += OnPassValue;

        manaSlider.maxValue = maxMana;
        currentMana = 0;
        ChangeValue();
    }

    void OnDisable()
    {
        ManaSystemEvents.OnManaAdded -= OnManaAdded;
        ManaSystemEvents.OnManaRemoved -= OnManaRemoved;
        ManaSystemEvents.OnPassValue -= OnPassValue;
    }

    void Update()
    {
        if (currentMana == maxMana) return;
        if (currentMana > maxMana) 
        {
            currentMana = maxMana;
            return; 
        }

        manaAddTimer += Time.deltaTime;
        if (manaAddTimer >= manaAddCooldown)
        {
            manaAddTimer = 0;
            OnManaAdded(manaAddValue);
        }
    }

    void OnManaAdded(int val)
    {
        currentMana += val;
        ValidateMana();
    }

    void OnManaRemoved(int val)
    {
        currentMana -= val;
        ValidateMana();
    }

    bool OnPassValue(int mana)
    {
        if (mana > currentMana)
        {
            return false;
        }
        else return true;
    }

    void ValidateMana()
    {
        ChangeValue();
    }

    public bool CanSpell(int cost)
    {
        if (cost <= currentMana) return true;
        else return false;
    }
}
