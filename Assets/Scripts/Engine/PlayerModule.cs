using UnityEngine;

public class PlayerModule : AppModule {

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
        runTimedEvent(5, delegate ()
        {
            Debug.Log("Callback");
        });
        // start event
    }

    private void addPlayerMovementListeners()
    {
        playerMovement.OnDirecetionChange += playerSpriteHandler.refreshAnimationParameters;
        playerMovement.OnMovementChange += playerSpriteHandler.refreshAnimationParameters;
    }
}
