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

    public Vector2Int getPlayerLocation()
    {
        Vector3 pos = playerBoxCollider.transform.position;
        Vector3Int coords3d = groundLayer.WorldToCell(pos);
        return new Vector2Int(coords3d.x, coords3d.y);
    }

    private void addPlayerMovementListeners()
    {
        //playerMovement.OnMoveDirectionChange += playerSpriteHandler.refreshAnimationParameters;
        playerMovement.OnLookDirectionChange += playerSpriteHandler.refreshAnimationParameters;
        playerMovement.OnMovementChange += playerSpriteHandler.refreshAnimationParameters;
    }
}
