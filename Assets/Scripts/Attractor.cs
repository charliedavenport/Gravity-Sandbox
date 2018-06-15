using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Attractor : MonoBehaviour {

	const float G_CONST = 1e-2f; // no need to use real physical value. Tune to make it 'feel' right

	public Rigidbody rb;

	public GameObject attrListObj;
	public List<Attractor> attrList;

	private void Start() {
		rb = GetComponent<Rigidbody> ();
		attrListObj = GameObject.Find ("Gravitational Bodies");
		attrList = new List<Attractor> (attrListObj.GetComponentsInChildren<Attractor> ());
	}

	private void FixedUpdate() {
		if (attrList.Count == 0)
			return;

		foreach (Attractor attr in attrList) {
			if (attr != this)
				Attract (attr);

		}
	}

	public void Attract(Attractor other) {
		Rigidbody rbOther = other.GetComponent<Rigidbody> ();
		Vector3 displacement = this.transform.position - other.transform.position;
		float distance = displacement.magnitude;

		float forceMagnitude = (rb.mass * rbOther.mass) / (distance * distance) * G_CONST; // F = G * m1 * m2 / r^2
		Vector3 force = displacement.normalized * forceMagnitude;

		rbOther.AddForce (force);
	}

	public void updateList() {
		attrList = new List<Attractor> (attrListObj.GetComponentsInChildren<Attractor> ());
	}


}
