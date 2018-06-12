using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalBody : MonoBehaviour {

    private Rigidbody rb;

    public Vector3 Pos {
        get {
            return transform.position;
        }
        set {
            transform.position = value;
        }
    }
    public Vector3 Vel {
        get {
            return rb.velocity;
        }
        set {
            rb.velocity = value;
        }
    }
    public float Mass {
        get {
            return rb.mass;
        }
    }

    public void Initialize(float mass, Vector3 start_pos, Vector3 start_vel) {
        transform.position = start_pos;
        if (rb == null) return;
        rb.mass = mass;
        rb.velocity = start_vel;
    }

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        //Debug.Log(rb.velocity);
    }

    private void OnDrawGizmos() {
        if (rb != null) {
            Gizmos.color = Color.red;
            // why is this behaving so weirdly?
            Gizmos.DrawLine(transform.position, (transform.position + rb.velocity).normalized);
        }
    }

}
