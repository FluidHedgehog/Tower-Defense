using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Tooltip("0 - For Right; 1 - For Down; 2 - For Left; 3 - For Up;")]
    [Range(0, 3)]
    public int currentAnimation;

    public string SetAnimation()
    {
        switch (currentAnimation)
        {
            case 0:
                return "Right";
            case 1:
                return "Down";
            case 2:
                return "Left";
            case 3:
                return "Up";
            case int:
                return "Right";
        }
    }
}
