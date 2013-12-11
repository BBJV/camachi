using UnityEngine;
using System.Collections;

public class garbageScript : MonoBehaviour {

    void Awake()
    {
        //rigidbody.AddForce(new Vector3(0, -100, 0), ForceMode.Force);
    }

	void Update () {
		// if fallSpeed is bigger then the eggs will fall quicker 
        float fallSpeed = 2 * Time.deltaTime;
		
		//let the garbage just fall on the road
		
		
        transform.position -= new Vector3(0, fallSpeed, 0);

        if (transform.position.y < -1)
        {
            //Destroy this gameobject (and all attached components)
            Destroy(gameObject);
        }
	}
}
