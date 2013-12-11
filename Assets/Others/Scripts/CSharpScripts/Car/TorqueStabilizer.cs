using UnityEngine;
using System.Collections;

public class TorqueStabilizer : MonoBehaviour {

    public float stability = 0.3f;
    public float speed = 2.0f;

    // Update is called once per frame
    void FixedUpdate () {
//		Debug.Log("rigidbody.angularVelocity.magnitude : " + rigidbody.angularVelocity.magnitude + " rigidbody.angularVelocity : " + rigidbody.angularVelocity);
        Vector3 predictedUp = Quaternion.AngleAxis(
            rigidbody.angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed,
            rigidbody.angularVelocity
        ) * transform.up;
//		Debug.Log(predictedUp);
        Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
        // Uncomment the next line to stabilize on only 1 axis.
        //torqueVector = Vector3.Project(torqueVector, transform.forward);
        rigidbody.AddTorque(torqueVector * speed);
    }
}