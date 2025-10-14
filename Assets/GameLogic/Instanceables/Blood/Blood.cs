using UnityEngine;

public class Blood : MonoBehaviour
{
    int bloodValue;

    public void Initialize(int blood)
    {
        bloodValue = blood;
    }

    public void OnMouseEnter()
    {
        Debug.Log("Added Blood!" + bloodValue);
        Destroy(gameObject);
    }
}
