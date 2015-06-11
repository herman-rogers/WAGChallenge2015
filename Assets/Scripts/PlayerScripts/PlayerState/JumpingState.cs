using UnityEngine;
using System.Collections;
using Mono;

public class JumpingState : PlayerState {

    private MainCharacterController playerController;
    private PlayerGroundCheck playerGroundCheck;
    private Rigidbody2D playerRigidbody;

    public JumpingState( GameObject player ) {
        playerController = player.GetComponent<MainCharacterController>( );
        playerRigidbody = player.GetComponent<Rigidbody2D>( );
        playerGroundCheck = player.GetComponentInChildren<PlayerGroundCheck>( );
    }

    public override void HandlePlayerInput( bool input ) { }

    public override void PlayerUpdate( GameObject player, bool input ) {
        if ( !input || !playerGroundCheck.isGrounded ) {
            return;
        }
        playerRigidbody.AddForce( new Vector2( 0, playerController.jumpForce ) );
        playerController.PlayAnimationOnce( "Jump" );
    }

}
