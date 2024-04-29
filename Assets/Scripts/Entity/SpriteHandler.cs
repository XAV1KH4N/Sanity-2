using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Animator animator;

    private static string HorizontalProperty = "Horizontal";
    private static string VerticalProperty = "Vertical";
    private static string IsMovingProperty = "IsMoving";
    private static string AttackProperty = "Attack";

    public void refreshAnimationParameters(Vector2 direction)
    {
        animator.SetFloat(HorizontalProperty, direction.x);
        animator.SetFloat(VerticalProperty, direction.y);
    }
    
    public void refreshAnimationParameters(bool isMoving)
    {
        animator.SetBool(IsMovingProperty, isMoving);
    }

    public void triggerAttack()
    {
        animator.SetTrigger(AttackProperty);
    }
}
