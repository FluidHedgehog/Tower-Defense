using UnityEngine;

public static class IdleEvents
{
    static Vector3Int currentTile;

    public static void OnPoint(Vector2 mousePos)
    {
        currentTile = GridHelper.ChangeToTile(mousePos);
    }

    public static void OnInteract()
    {

    }

    public static void OnHold()
    {
        
    }

    public static void OnRelease()
    {

    }
}
