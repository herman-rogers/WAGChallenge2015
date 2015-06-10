using UnityEngine;
using System.Collections;

public class PlayerDebugInformation : MonoBehaviour {

    public GameObject playerCharacter;
    private MainCharacterController characterController;
    private PlayerObjectDetection playerDebugObjectDetection;
    private RaycastHit2D playerHitInformation;
    private Rigidbody2D playerRigidbody;
    private float playerVelocity;
    private string objectCurrentlyCollidingWith = "N/A";
    private string currentActionToTake;
    private bool hideText = true;

    private void Start( ) {
        characterController = playerCharacter.GetComponent<MainCharacterController>( );
        playerDebugObjectDetection = playerCharacter.GetComponent<PlayerObjectDetection>( );
        playerRigidbody = playerCharacter.GetComponent<Rigidbody2D>( );
    }

    private void FixedUpdate( ) {
        ObjectDetectionDebugInformation( );
        playerVelocity = playerRigidbody.velocity.x;
        playerHitInformation = playerDebugObjectDetection.currentHitObject;
    }

    private void OnGUI( ) {
        hideText = GUI.Toggle( new Rect( 10, 10, 200, 200 ), hideText, "Toggle Debug Info" );
        if ( !hideText ) {
            return;
        }
        SliderBoxes( );
        PlayerInfoBox( );
    }

    private void SliderBoxes( ) {
        GUI.Box( new Rect( 10, 350, 300, 200 ), "Player Controller Info" );
        GUI.Label( new Rect( 20, 370, 300, 20 ), "Max Speed: " + characterController.maxSpeed );
        characterController.maxSpeed = GUI.HorizontalSlider( new Rect( 20, 400, 200, 30 ), characterController.maxSpeed, 0.0F, 1000.0F );
        GUI.Label( new Rect( 20, 420, 300, 20 ), "Acceleration Rate: " + characterController.accelerationRate );
        characterController.accelerationRate = GUI.HorizontalSlider( new Rect( 20, 450, 200, 30 ), characterController.accelerationRate, 0.0F, 1000.0F );
    }

    private void PlayerInfoBox( ) {
        GUI.TextArea( new Rect( 10, 40, 300, 200 ), "Player Controller Info" );
        GUI.Label( new Rect( 20, 70, 300, 20 ), "On The Ground: " + characterController.isGrounded );
        GUI.Label( new Rect( 20, 90, 300, 20 ), "Object Hit: " + objectCurrentlyCollidingWith );
        GUI.Label( new Rect( 20, 110, 300, 20 ), "Object Distance: " + playerHitInformation.distance );
        GUI.Label( new Rect( 20, 130, 300, 20 ), "Acceleration: " + playerVelocity );
        GUI.Label( new Rect( 20, 150, 300, 20 ), "Action: " + currentActionToTake );
    }

    private void ObjectDetectionDebugInformation( ) {
        if ( playerHitInformation.collider != null ) {
            objectCurrentlyCollidingWith = playerHitInformation.collider.name;
            CheckForActionsToTake( );
            Debug.DrawLine( characterController.transform.position, playerHitInformation.point, Color.white );
        } else {
            ClearDebugInfo( );
        }
    }

    private void CheckForActionsToTake( ) {
        if ( ( playerHitInformation.distance > 7.0f && playerHitInformation.distance < 13.0f ) ) {
            currentActionToTake = "PRESS S NOW!! < SPEED VAULT >";
        }
    }

    private void ClearDebugInfo( ) {
        objectCurrentlyCollidingWith = "N/A";
        currentActionToTake = "";
    }
}