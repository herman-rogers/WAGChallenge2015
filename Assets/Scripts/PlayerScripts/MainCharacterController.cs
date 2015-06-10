using UnityEngine;
using System.Collections;

public class MainCharacterController : MonoBehaviour {

    public float maxSpeed = 10.0f;
    public float jumpForce = 400;
    public float speedVault = 250;
    public float accelerationRate = 1.0f;
    public bool isGrounded = false;
    private Rigidbody2D characterRigidbody;
    private PlayerObjectDetection hitObject;
    private Animator characterAnimator;
    private bool facingRight = true;
    private float accelerationMultiplier = 0.0f;
    private float acceleration;

    private void Start( ) {
        characterRigidbody = GetComponent<Rigidbody2D>( );
        characterAnimator = GetComponent<Animator>( );
        hitObject = GetComponent<PlayerObjectDetection>( );
    }

    private void FixedUpdate( ) {
        float move = Input.GetAxis( "Horizontal" );

        if ( isGrounded ) {
            MoveCharacter( move );
        }

    }

    private void Update( ) {
        if ( isGrounded && Input.GetKeyDown( KeyCode.Space ) ) {
            StartCoroutine( PlayerAnimationOnce( "Jump" ) );
            GetComponent<Rigidbody2D>( ).AddForce( new Vector2( 0, jumpForce ) );
        }
        if ( isGrounded && Input.GetKeyDown( KeyCode.S ) ) {
            GetComponent<Rigidbody2D>( ).AddForce( new Vector2( 0, speedVault ) );
            StartCoroutine( PlayerAnimationOnce( "SpeedVault" ) );
            if ( ( hitObject.currentHitObject.distance > 7.0f && hitObject.currentHitObject.distance < 13.0f ) ) {
                StartCoroutine( TemporarilyDisableCollider( hitObject.currentHitObject ) );
            }
        }
    }

    private IEnumerator PlayerAnimationOnce( string animationName ) {
        characterAnimator.SetBool( animationName, true );
        yield return null;
        characterAnimator.SetBool( animationName, false );
    }

    private IEnumerator TemporarilyDisableCollider( RaycastHit2D hit ) {
        hit.collider.enabled = false;
        yield return new WaitForSeconds( 1.0f );
        hit.collider.enabled = true;
    }

    private void MoveCharacter( float move ) {
        float characterMovementSpeed = Mathf.Abs( move );
        characterAnimator.SetFloat( "Speed", characterMovementSpeed );
        characterRigidbody.velocity = new Vector2( ( move * acceleration ),
                                                     characterRigidbody.velocity.y );
        ChangeAcceleration( characterMovementSpeed );
        ChangeFacingDirection( move );
    }

    private void ChangeAcceleration( float movement ) {
        if ( acceleration < maxSpeed && movement > 0.0f ) {
            acceleration += ( Time.deltaTime + ( accelerationRate / 100 ) );
        } else if ( movement == 0 ) {
            acceleration = 0;
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
        acceleration = 0;
        Vector3 facingDirection = transform.localScale;
        facingDirection.x *= -1;
        transform.localScale = facingDirection;
    }
}