using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerController : PlayerController {

	public GameObject SteamVR_Rig;
	public SteamVR_TrackedObject hmd;
	public SteamVR_TrackedObject controllerL;
	public SteamVR_TrackedObject controllerR;

	public Transform head;
	public Transform handL;
	public Transform handR;

	//public GameObject planetPrefab;
	//public GameObject AttractorsObj;

	private bool a_btn_down;
    private bool a_btn_up;
	private float triggerL;
	private float triggerR;

	private void Awake() {
		AttractorsObj = GameObject.Find ("Attractors");
	}

	private void Update() {
		handleControllerInputs ();
		if (a_btn_down)
			StartCoroutine (doSpawnPlanet (handR.transform.position));
	}

	private void FixedUpdate() {
		copyTransform (hmd.transform, head);
		copyTransform (controllerL.transform, handL);
		copyTransform (controllerR.transform, handR);
	}

	private void handleControllerInputs() {
		int indexL = (int)controllerL.index;
		int indexR = (int)controllerR.index;

		triggerL = getTrigger (controllerL);
		triggerR = getTrigger (controllerR);

		//a_btn_down = SteamVR_Controller.Input (indexR).GetPressDown (Valve.VR.EVRButtonId.k_EButton_A);
		a_btn_down = SteamVR_Controller.Input (indexR).GetPressDown (Valve.VR.EVRButtonId.k_EButton_Grip);
        a_btn_up = SteamVR_Controller.Input(indexR).GetPressUp(Valve.VR.EVRButtonId.k_EButton_Grip);

    }

	private float getTrigger(SteamVR_TrackedObject con) {
		return con.index >= 0 ?
			SteamVR_Controller.Input ((int)con.index).GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).magnitude : 
			0f;
	}

	private void copyTransform(Transform from, Transform to) {
		to.position = from.position;
		to.rotation = from.rotation;
	}

	private IEnumerator doSpawnPlanet(Vector3 pos) {
        // initially, the planet is unparented. This prevents it from interacting with other attractors
        // until the user is finished placing it.
		GameObject planet = GameObject.Instantiate (planetPrefab, pos, Quaternion.identity);
        Rigidbody planetRB = planet.GetComponent<Rigidbody>();
        VelocityLine planetVelocityLine = planet.GetComponent<VelocityLine>();
        Attractor planetAttr = planet.GetComponent<Attractor>();

        planetRB.isKinematic = true; // RigidBody.addForce() will not affect planet
        planetAttr.enabled = false; // disable attractor script to prevent this planet from affecting others
        planetVelocityLine.enabled = true;

        yield return null;
        Vector3 velocity = Vector3.zero;
        while (!a_btn_up) // wait for a_btn release
        {
            // determine starting velocity and draw arrow
            velocity = - (handR.position - planet.transform.position);
            planetVelocityLine.EndPoint = planet.transform.position + velocity;
            yield return null;
        }

        planetAttr.enabled = true;
        planetVelocityLine.lr.enabled = false; // don't show the velocity line anymore
        planetRB.velocity = velocity; // apply velocity to the rigidbody
        planet.GetComponent<Rigidbody>().isKinematic = false; // RigidBody.addForce() will affect planet
        planet.transform.SetParent(AttractorsObj.transform); // Now the simulation will consider this planet
        planet.GetComponent<Attractor>().enabled = true;

        List<Attractor> attrList = new List<Attractor>(AttractorsObj.GetComponentsInChildren<Attractor>());
        foreach (Attractor attr in attrList)
        {
            attr.updateList();
        }

    }

}
