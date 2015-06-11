using UnityEngine;
using System.Collections;

public class KongState : PlayerState {

    private MainCharacterController playerController;
    private Rigidbody2D playerRigidbody;
    private PlayerGroundCheck playerGroundCheck;
    private PlayerObjectDetection objectDetection;

    public KongState( GameObject player ) {
        playerController = player.GetComponent<MainCharacterController>( );
        playerRigidbody = player.GetComponent<Rigidbody2D>( );
        playerGroundCheck = player.GetComponentInChildren<PlayerGroundCheck>( );
        objectDetection = player.GetComponentInChildren<PlayerObjectDetection>( );
    }

    public override void HandlePlayerInput( bool input ) { }

    public override void PlayerUpdate( GameObject player, bool input ) {
        if ( !input || !playerGroundCheck.isGrounded ) {
            return;
        }

        if ( ( objectDetection.currentHitObject.distance > 7.0f &&
               objectDetection.currentHitObject.distance < 13.0f ) ) {
            playerRigidbody.AddForce( new Vector2( 0, playerController.kongVault ) );
            playerController.PlayAnimationOnce( "KongVault" );
        }
    }
}
