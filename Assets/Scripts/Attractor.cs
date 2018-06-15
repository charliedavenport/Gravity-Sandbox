using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Attractor : MonoBehaviour {

	const float G_CONST = 2e-2f; // no need to use real physical value. Tune to make it 'feel' right

	public Rigidbody rb;

	public GameObject planetPrefab;
	public GameObject attrListObj;
	public List<Attractor> attrList;

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

	private void Awake() {
		rb = GetComponent<Rigidbody>();
		attrListObj = GameObject.Find("Attractors");
		attrList = new List<Attractor>(attrListObj.GetComponentsInChildren<Attractor>());
		if (attrList == null) {
			attrList = new List<Attractor> ();
			attrList.Add (this);
		}
	}

	private void FixedUpdate() {
		if (attrList.Count == 0)
			return;

		foreach (Attractor attr in attrList) {
			if (attr != this && attr != null)
				Attract (attr);

		}
	}

	private void Merge(Attractor other){
		float m1 = this.rb.mass;
		float m2 = other.rb.mass;
		Vector3 v1 = this.rb.velocity;
		Vector3 v2 = other.rb.velocity;
		this.rb.velocity = (m1 * v1 + m2 * v2) / (m1 + m2);
		this.rb.mass += m2;
		this.transform.localScale = this.transform.localScale + new Vector3(.1f, .1f, .1f); //TODO: redo this. Reasearch relationship between mass and planet radius
		Destroy (other.gameObject);

	}


	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Attractor") {
			Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody> ();
			if (this.rb.mass > otherRB.mass) { // prefer the most massive body
				Merge(other.gameObject.GetComponent<Attractor>());
			}
			else if (this.rb.mass == otherRB.mass && this.transform.position.z > other.transform.position.z) { // randomly pick one planet. not the most elegant solution but it seems to work
				Merge(other.gameObject.GetComponent<Attractor>());
			}

		}
	}






}
