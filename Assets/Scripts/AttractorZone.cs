using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorZone : MonoBehaviour {

	public void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Attractor")
			Destroy (other.gameObject);
	}

}
