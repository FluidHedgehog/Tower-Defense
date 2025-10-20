using UnityEngine;
using UnityEngine.UI;

public static class BloodSystemEvents
{
    public static event System.Action<int> OnBloodAdded;
    public static event System.Action<int> OnBloodRemoved;

    public static void TriggerBloodAdded(int val) => OnBloodAdded?.Invoke(val);
    public static void TriggerBloodRemoved(int val) => OnBloodRemoved?.Invoke(val);
}

public class BloodSystem : MonoBehaviour
{
    [SerializeField] Slider bloodSlider;

    [SerializeField] int maxBlood;

    int currentBlood { get; set; }

    void ChangeValue()
    {
        bloodSlider.value = currentBlood;
    }

    void OnEnable()
    {
        BloodSystemEvents.OnBloodAdded += OnBloodAdded;
        BloodSystemEvents.OnBloodRemoved += OnBloodRemoved;

        bloodSlider.maxValue = maxBlood;
        currentBlood = maxBlood;
        ChangeValue();
    }

    void OnDisable()
    {
        BloodSystemEvents.OnBloodAdded -= OnBloodAdded;
        BloodSystemEvents.OnBloodRemoved -= OnBloodRemoved;
    }

    void OnBloodAdded(int val)
    {
        currentBlood += val;
        ValidateBlood();
    }

    void OnBloodRemoved(int val)
    {
        currentBlood -= val;
        ValidateBlood();
    }

    void ValidateBlood()
    {
        ChangeValue();
    }
}
