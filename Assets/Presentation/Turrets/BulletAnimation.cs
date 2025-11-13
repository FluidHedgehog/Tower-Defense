using System.Collections;
using UnityEngine;

public class BulletAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    public float swapInterval = 0.5f;

    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private float timer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(SwapSprites());
    }

    private IEnumerator SwapSprites()
    {
        while (true)
        {
            currentIndex = (currentIndex + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[currentIndex];
            yield return new WaitForSeconds(swapInterval);
        }
    }
}
