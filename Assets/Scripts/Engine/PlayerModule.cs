using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerModule : AppModule {

    [SerializeField]
    private CharacterMovement playerMovement;

    [SerializeField]
    private SpriteHandler playerSpriteHandler;

    [SerializeField]
    private AttackAction attackAction;

    [SerializeField]
    private BoxCollider2D playerBoxCollider;

    [SerializeField]
    private Tilemap groundLayer;

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

    public Vector3Int getPlayerLocation()
    {
        Vector3 pos = playerBoxCollider.transform.position;
        return groundLayer.WorldToCell(pos);
    }

    private void addPlayerMovementListeners()
    {
        playerMovement.OnDirecetionChange += playerSpriteHandler.refreshAnimationParameters;
        playerMovement.OnMovementChange += playerSpriteHandler.refreshAnimationParameters;
    }
}
