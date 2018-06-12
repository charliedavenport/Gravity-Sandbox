using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulation : MonoBehaviour {

    // normally, this constant is e-11 and masses are e11, 
    // but I'm keeping the scale at around 1 to keep things simple
    private const float G_CONST = (float)6.673f;
    // to avoid singularities, there is a max accelleration (magnitude) that can be applied to a body
    private const float MAX_ACCELL = 100f;
    private const float MIN_RADIUS = 0.2f;

    [SerializeField] private GameObject bodiesObj;
    [SerializeField] private List<GravitationalBody> bodies;

    private List<float> accell_values;

    private void Awake() {
        bodies = new List<GravitationalBody>(bodiesObj.GetComponentsInChildren<GravitationalBody>());

        accell_values = new List<float>();
    }

    private void FixedUpdate() {
        if (bodies == null) return;

        // run simulation
        Vector3 accell;
        for (int i = 0; i < bodies.Count; i++) {
            accell = getAccelleration(i);
            bodies[i].Vel += accell * Time.deltaTime; // update velocity
        }
        for (int i = 0; i < bodies.Count; i++) {
            bodies[i].Pos += bodies[i].Vel * Time.deltaTime; // update postition
        }
    }

    private Vector3 getAccelleration(int target_ind) {

        Vector3 accell = new Vector3();
        GravitationalBody target = bodies[target_ind];
        // brute force approach - can be much more effecient!
        for (int i=0; i<bodies.Count; i++) {
            GravitationalBody current_body = bodies[i];
            if (i != target_ind) {
                Vector3 differential = current_body.Pos - target.Pos;
                float r = differential.magnitude;
                if (r > MIN_RADIUS)
                    accell += differential * G_CONST * current_body.Mass / Mathf.Pow(r, 3);
            }
        }
        // TODO: bounds checking on accelleration (avoid singularities)
        //accell_values.Add(accell.magnitude);
        //if (accell.magnitude > MAX_ACCELL) accell = Vector3.zero;
        return accell;
    }

    /*
    private void OnApplicationQuit() {
        // stats regarding accelleration values

        float min = Mathf.Min(accell_values.ToArray());
        float max = Mathf.Max(accell_values.ToArray());
        float avg = 0f;
        foreach(float f in accell_values) {
            avg += f;
        }
        avg /= accell_values.Count;

        Debug.Log(string.Format("Min: {0}\nMax: {1}\nAvg: {2}\n", min, max, avg));
    }
    //*/

}
