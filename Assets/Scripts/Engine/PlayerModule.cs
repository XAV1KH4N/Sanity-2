using UnityEngine;

public class PlayerModule : MonoBehaviour {

    [SerializeField]
    private CharacterMovement playerMovement;

    [SerializeField]
    private SpriteHandler playerSpriteHandler;

    [SerializeField]
    private AttackAction attackAction;

    void Start()
    {
        addPlayerMovementListeners();
    }

    public void swingSword()
    {
        playerSpriteHandler.triggerAttack();
    }

    private void addPlayerMovementListeners()
    {
        playerMovement.OnDirecetionChange += playerSpriteHandler.refreshAnimationParameters;
        playerMovement.OnMovementChange += playerSpriteHandler.refreshAnimationParameters;
    }
}
