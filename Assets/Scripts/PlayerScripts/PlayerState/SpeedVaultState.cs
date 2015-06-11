using UnityEngine;
using System.Collections;

public class SpeedVaultState : PlayerState {

    private MainCharacterController playerController;
    private Rigidbody2D playerRigidbody;
    private PlayerGroundCheck playerGroundCheck;
    private PlayerObjectDetection objectDetection;

    public SpeedVaultState( GameObject player ) {
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
        playerRigidbody.AddForce( new Vector2( 0, playerController.speedVault ) );
        playerController.PlayAnimationOnce( "SpeedVault" );

        if ( ( objectDetection.currentHitObject.distance > 7.0f && 
               objectDetection.currentHitObject.distance < 13.0f ) ) {
            objectDetection.DisableHitObject( );
        }
    }

}
