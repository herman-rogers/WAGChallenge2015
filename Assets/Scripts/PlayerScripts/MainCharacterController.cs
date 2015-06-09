using UnityEngine;
using System.Collections;

public class MainCharacterController : MonoBehaviour {

    public float maxSpeed = 10.0f;
    public float acceleration = 1.0f;
    public float jumpForce = 400;
    public bool isGrounded = false;
    private Rigidbody2D characterRigidbody;
    private Animator characterAnimator;
    private float accelerationMultiplier = 0.0f;
    private bool facingRight = true;

    private void Start( ) {
        characterRigidbody = GetComponent<Rigidbody2D>( );
        characterAnimator = GetComponent<Animator>( );
    }

    private void FixedUpdate( ) {
        float move = Input.GetAxis( "Horizontal" );
        if ( isGrounded ) {
            MoveCharacter( move );
        }
    }

    private void Update( ) {
        if ( isGrounded && Input.GetKeyDown( KeyCode.Space ) ) {
            characterAnimator.SetBool( "Ground", false );
            GetComponent<Rigidbody2D>( ).AddForce( new Vector2( 0, jumpForce ) );
        } else {
            characterAnimator.SetBool( "Ground", isGrounded );
        }
    }

    private void MoveCharacter( float move ) {
        float normalizeMove = Mathf.Abs( move );
        characterAnimator.SetFloat( "Speed", normalizeMove );
        characterRigidbody.velocity = new Vector2( ( move * maxSpeed ),
                                                     characterRigidbody.velocity.y ); //accelerationMultiplier
        ChangeAcceleration( normalizeMove );
        ChangeFacingDirection( move );
    }

    private void ChangeAcceleration( float movement ) {
        if ( movement > 0 && movement < maxSpeed ) {
            accelerationMultiplier += ( Time.deltaTime * ( acceleration ) );
        } else if ( movement == 0 ) {
            accelerationMultiplier = 0;
        }
    }

    private void ChangeFacingDirection( float movement ) {
        if ( movement > 0 && !facingRight ) {
            InvertDirection( );
        } else if ( movement < 0 && facingRight ) {
            InvertDirection( );
        }
    }

    private void InvertDirection( ) {
        facingRight = !facingRight;
        accelerationMultiplier = 0;
        Vector3 facingDirection = transform.localScale;
        facingDirection.x *= -1;
        transform.localScale = facingDirection;
    }
}