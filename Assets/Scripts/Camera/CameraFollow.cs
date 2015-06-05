using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform followTarget;
	
	void FixedUpdate () {
        Vector3 targetsPosition = new Vector3( followTarget.position.x, 
                                               followTarget.position.y, 
                                               transform.position.z );
        transform.position = targetsPosition;
	}
}
