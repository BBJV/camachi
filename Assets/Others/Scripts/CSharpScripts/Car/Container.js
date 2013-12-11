var com : Transform;
var rearWheels : Transform[];
var frontWheels : Transform[];
var resetTime : float = 5.0f;
private var resetTimer : float = 0.0f;

function Start () {
	rigidbody.centerOfMass = com.localPosition;
}

function Update () {
//	for(var w : Transform in rearWheels)
//	{
//		w.GetChild(0).Rotate(Vector3.right * Time.deltaTime * Mathf.Rad2Deg);
//	}
	Check_If_Car_Is_Flipped();
}

function Check_If_Car_Is_Flipped()
{
	if(transform.localEulerAngles.z > 80 && transform.localEulerAngles.z < 280)
		resetTimer += Time.deltaTime;
	else
		resetTimer = 0;
	
	if(resetTimer > resetTime)
		FlipCar();
}

function FlipCar()
{
	transform.rotation = Quaternion.LookRotation(transform.forward);
	transform.position += Vector3.up * 0.5f;
	hingeJoint.connectedBody.transform.position = transform.position;
	hingeJoint.connectedBody.transform.rotation = transform.rotation;
	hingeJoint.connectedBody.hingeJoint.connectedBody.transform.position = transform.position;
	hingeJoint.connectedBody.hingeJoint.connectedBody.transform.rotation = transform.rotation;
	rigidbody.velocity = Vector3.zero;
	rigidbody.angularVelocity = Vector3.zero;
	hingeJoint.connectedBody.velocity = rigidbody.velocity;
	hingeJoint.connectedBody.angularVelocity = rigidbody.angularVelocity;
	hingeJoint.connectedBody.hingeJoint.connectedBody.velocity = rigidbody.velocity;
	hingeJoint.connectedBody.hingeJoint.connectedBody.angularVelocity = rigidbody.angularVelocity;
	resetTimer = 0;
}