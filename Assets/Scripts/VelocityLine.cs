using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VelocityLine : MonoBehaviour {

    public LineRenderer lr;

    public Vector3 EndPoint;
    
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lr.receiveShadows = false;
        lr.positionCount = 2;
        lr.SetPositions(new Vector3[] { this.transform.position, this.transform.position });
    }

    private void Start()
    {
        //lr.enabled = false;
    }

    private void LateUpdate()
    {
        if (lr.enabled)
        {
            lr.SetPosition(1, EndPoint);
        }
    }


}
