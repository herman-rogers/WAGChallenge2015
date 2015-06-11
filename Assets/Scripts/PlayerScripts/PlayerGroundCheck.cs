using UnityEngine;
using System.Collections;

public class PlayerGroundCheck : MonoBehaviour {

    public LayerMask whatIsGround;
    public bool isGrounded = true;
    private MainCharacterController playerController;

    private void Start( ) {
        playerController = GetComponentInParent<MainCharacterController>( );
    }

    private void FixedUpdate( ) {
        playerController.isGrounded = Physics2D.OverlapCircle( transform.position, 0.2f, whatIsGround );
        isGrounded = Physics2D.OverlapCircle( transform.position, 0.2f, whatIsGround );
    }
}