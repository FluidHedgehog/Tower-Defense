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
        currentMana = maxMana;
        ChangeValue();
    }

    void OnDisable()
    {
        ManaSystemEvents.OnManaAdded -= OnManaAdded;
        ManaSystemEvents.OnManaRemoved -= OnManaRemoved;
        ManaSystemEvents.OnPassValue -= OnPassValue;
    }

    void OnManaAdded(int val)
    {
        currentMana += val;
        ValidateBlood();
    }

    void OnManaRemoved(int val)
    {
        currentMana -= val;
        ValidateBlood();
    }

    bool OnPassValue(int blood)
    {
        if (blood > currentMana)
        {
            return false;
        }
        else return true;
    }

    void ValidateBlood()
    {
        ChangeValue();
    }
}
