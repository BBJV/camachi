using UnityEngine;
using System.Collections;

public class CarCamera : MonoBehaviour
{
	public Transform target = null;
	public float height = 1f;
	public float positionDamping = 3f;
	public float velocityDamping = 3f;
	public float rotationDamping = 3f;
	public float heightDamping = 3f;
	public float distance = 4f;
	public LayerMask ignoreLayers = -1;

	private RaycastHit hit = new RaycastHit();

	private Vector3 prevVelocity = Vector3.zero;
	private LayerMask raycastLayers = -1;
	
	private Vector3 currentVelocity = Vector3.zero;
	
	void Start()
	{
		raycastLayers = ~ignoreLayers;
		//target.position = target.position + Vector3.up * height + target.TransformDirection(Vector3.forward)  * distance;
	}

	void FixedUpdate()
	{
		/*currentVelocity = Vector3.Lerp(prevVelocity, target.root.rigidbody.velocity, velocityDamping * Time.deltaTime);
		currentVelocity.y = 0;
		print("before = "+target.transform.InverseTransformDirection(target.root.rigidbody.velocity).z);
		prevVelocity = currentVelocity;*/
		Vector3 newTargetPosition = target.position + Vector3.up * height;
		Vector3 newPosition = newTargetPosition + target.TransformDirection(Vector3.forward)  * distance;
		currentVelocity = newTargetPosition - newPosition;
		currentVelocity = Vector3.Lerp(prevVelocity, currentVelocity, velocityDamping * Time.deltaTime);
		currentVelocity.y = 0;
		
		prevVelocity = currentVelocity;
		//print("before rigidbody = "+target.root.rigidbody.velocity);
		
		/*if(target.InverseTransformDirection(target.root.rigidbody.velocity).z > 0){
			//print("before = "+currentVelocity);
			currentVelocity = -1.0f * currentVelocity;
			//print("after = "+currentVelocity);
		}*/
		
		//print(transform.InverseTransformDirection(target.root.rigidbody.velocity).z);
	}
	
	void LateUpdate()
	{
		
		float speedFactor = Mathf.Clamp01(target.root.rigidbody.velocity.magnitude / 70.0f);
		camera.fieldOfView = Mathf.Lerp(55, 72, speedFactor);
		float currentDistance = Mathf.Lerp(7.5f, 6.5f, speedFactor);
		
		currentVelocity = currentVelocity.normalized;
		
		//Vector3 newTargetPosition = target.position + Vector3.up * height;
		Vector3 newTargetPosition = target.position ;
		
		//Vector3 newPosition = newTargetPosition - (currentVelocity * currentDistance);/
		Vector3 newPosition = newTargetPosition - (currentVelocity * currentDistance)+ target.TransformDirection(Vector3.forward)  * distance;
		newPosition.y = newTargetPosition.y + height;
		
		
		Vector3 targetDirection = newPosition - newTargetPosition;
		//newPosition = Vector3.Lerp(transform.position,newPosition,0.2f);
		if(Physics.Raycast(newTargetPosition, targetDirection, out hit, currentDistance, raycastLayers))
			newPosition = hit.point;
		
		transform.position = newPosition;
		transform.LookAt(newTargetPosition);
		
		
		
		//Vector3 newPosition = newTargetPosition + target.TransformDirection(Vector3.forward)  * distance;
		/*Vector3 newPosition = newTargetPosition + target.TransformDirection(Vector3.forward)  * distance;
		float wantedRotationAngle = target.eulerAngles.y;
		float currentRotationAngle = transform.eulerAngles.y;
		print("wantedRotationAngle="+wantedRotationAngle);
		print("currentRotationAngle="+currentRotationAngle);
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		print("currentRotationAngle="+currentRotationAngle);
		Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		newPosition = newTargetPosition + currentRotation * target.TransformDirection(Vector3.forward)  * distance;*/
		//newPosition = Vector3.RotateTowards(transform.position,
		 //                                   newPosition,1f,2f);
		// Early out if we don't have a target
    /*if (!target)
        return;
		
	    // Calculate the current rotation angles
	    float wantedRotationAngle = target.eulerAngles.y;
	    float wantedHeight = target.position.y + height;
	
	    float currentRotationAngle = transform.eulerAngles.y;
	    float currentHeight = transform.position.y;
	
	    // Damp the rotation around the y-axis
	    currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
	    // Damp the height
	    currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
	
	    // Convert the angle into a rotation
	    Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
	
	    // Set the position of the camera on the x-z plane to:
	    // distance meters behind the target
	    transform.position = target.position;
	    transform.position -= currentRotation * Vector3.back * distance;
	    // Set the height of the camera
	    transform.position =new Vector3(transform.position.x,currentHeight,transform.position.z); 
	
	    // Always look at the target
	    transform.LookAt (target);*/

		
	}
}
