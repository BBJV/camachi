using UnityEngine;
using System.Collections;

public class DeathSpace : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
        if(other.transform.root.GetComponent<CarB>())
		{
			StartCoroutine(other.transform.root.GetComponent<CarB>().FlipCar());
		}
    }
}
