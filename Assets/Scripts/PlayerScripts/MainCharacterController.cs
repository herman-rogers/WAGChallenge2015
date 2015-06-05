using UnityEngine;
using System.Collections;

public class MainCharacterController : MonoBehaviour {

    public float maxSpeed = 10.0f;
    private Rigidbody2D characterRigidbody;
    private Animator characterAnimator;
    private bool facingRight = true;

    private void Start( ) {
        characterRigidbody = GetComponent<Rigidbody2D>( );
        characterAnimator = GetComponent<Animator>( );
    }

    private void FixedUpdate( ) {
        float move = Input.GetAxis("Horizontal");
        characterAnimator.SetFloat( "Speed", Mathf.Abs( move ) );

        characterRigidbody.velocity = new Vector2( move * maxSpeed, characterRigidbody.velocity.y );
        ChangeFacingDirection( move );
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
        Vector3 facingDirection = transform.localScale;
        facingDirection.x *= -1;
        transform.localScale = facingDirection;
    }
}