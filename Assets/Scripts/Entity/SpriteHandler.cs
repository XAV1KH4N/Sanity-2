using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public void matchSpriteWithDirection(Vector2 direction)
    {
        spriteRenderer.flipX = direction.x < 0;
    }
}
