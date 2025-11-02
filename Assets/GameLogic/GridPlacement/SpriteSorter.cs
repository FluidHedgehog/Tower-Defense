using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void LateUpdate()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }

}
