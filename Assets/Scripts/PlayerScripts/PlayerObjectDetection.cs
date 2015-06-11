using UnityEngine;
using System.Collections;

public class PlayerObjectDetection : MonoBehaviour {

    public LayerMask objectsToIgnore;
    public RaycastHit2D currentHitObject;

    private void FixedUpdate( ) {
        Vector3 currentFacingDirection = transform.localScale;
        Vector3 checkFacingDirection = new Vector3( currentFacingDirection.x, 0, 0 );
        currentHitObject = Physics2D.Raycast( transform.position, checkFacingDirection, 20, objectsToIgnore );
    }

    public void DisableHitObject( ) {
        StartCoroutine( TemporarilyDisableCollider( currentHitObject ) );
    }

    private IEnumerator TemporarilyDisableCollider( RaycastHit2D hit ) {
        hit.collider.enabled = false;
        yield return new WaitForSeconds( 1.0f );
        hit.collider.enabled = true;
    }

}
