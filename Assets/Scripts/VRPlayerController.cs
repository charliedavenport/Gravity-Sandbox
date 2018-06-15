using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerController : MonoBehaviour {

	public GameObject SteamVR_Rig;
	public SteamVR_TrackedObject hmd;
	public SteamVR_TrackedObject controllerL;
	public SteamVR_TrackedObject controllerR;

	public Transform head;
	public Transform handL;
	public Transform handR;

	public GameObject planetPrefab;
	public GameObject GravitationalBodies;

	private bool a_btn_down;
	private float triggerL;
	private float triggerR;

	private void Awake() {
		GravitationalBodies = GameObject.Find ("Gravitational Bodies");
	}

	private void Update() {
		handleControllerInputs ();
		if (a_btn_down)
			spawnPlanet (handR.transform.position);
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

	private void spawnPlanet(Vector3 pos) {
		GameObject.Instantiate (planetPrefab, pos, Quaternion.identity, GravitationalBodies.transform);
		List<Attractor> attrList = new List<Attractor> (GravitationalBodies.GetComponentsInChildren<Attractor> ());
		foreach (Attractor attr in attrList) {
			attr.updateList ();
		}
	}

}
