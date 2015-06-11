using UnityEngine;
using System.Collections;

public class MainCharacterController : MonoBehaviour {

    public float maxSpeed = 10.0f;
    public float jumpForce = 400;
    public float speedVault = 250;
    public float dashVault = 250;
    public float kongVault = 250;
    public float accelerationRate = 1.0f;
    public bool isGrounded = false;
    private Rigidbody2D characterRigidbody;
    private Animator characterAnimator;
    private bool facingRight = true;
    private float acceleration;

    private PlayerState _state;
    private string activeInput;

    private void Start( ) {
        _state = new PlayerState( );
        PlayerState._jumpState = new JumpingState( this.gameObject );
        PlayerState._speedVault = new SpeedVaultState( this.gameObject );
        PlayerState._dashVault = new DashVaultState( this.gameObject );
        PlayerState._kongVault = new KongState( this.gameObject );
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

        if ( Input.GetButtonDown( "Jump" ) ) {
            _state = PlayerState._jumpState;
            activeInput = "Jump";
        }
        if ( Input.GetButtonDown( "SpeedVault" ) ) {
            _state = PlayerState._speedVault;
            activeInput = "SpeedVault";
        }
        if ( Input.GetButtonDown( "DashVault" ) ) {
            _state = PlayerState._dashVault;
            activeInput = "DashVault";
        }
        //if ( Input.GetButtonDown( "KongVault" ) ) {
        //    _state = PlayerState._kongVault;
        //    activeInput = "KongVault";
        //}

        if ( activeInput == null || Mathf.Abs( characterRigidbody.velocity.x ) <= 0.0f ) {
            Debug.Log( characterRigidbody.velocity.x );
            return;
        }
        _state.HandlePlayerInput( this.gameObject );
        _state.PlayerUpdate( this.gameObject, Input.GetButtonDown( activeInput ) );
    }

    public void PlayAnimationOnce( string animation ) {
        StartCoroutine( AnimationOneShot( animation ) );
    }

    private IEnumerator AnimationOneShot( string animation ) {
        characterAnimator.SetBool( animation, true );
        yield return null;
        characterAnimator.SetBool( animation, false );
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