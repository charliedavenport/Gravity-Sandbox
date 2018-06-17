using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimulationManager : MonoBehaviour, IGameManager {

    public ManagerStatus status { get; private set; }
    public int n_attr { get; private set; }

    const float G_CONST = 2e-2f; // no need to use real physical value. Tune to make it 'feel' right
    const int MAX_ATTR = 50; // maximum allowed # of attractors

    private List<Attractor> attrList;
    // Matrix containing forces to be applied to each Attractor every fixed update
    // symmetrical along main diagonal
    private Vector3[,] ForceMatrix; 

    public void Startup() {
        status = ManagerStatus.Initializing;
        attrList = new List<Attractor>(GetComponentsInChildren<Attractor>());
        if (attrList == null) attrList = new List<Attractor>();
        n_attr = attrList.Count;
        ForceMatrix = new Vector3[MAX_ATTR, MAX_ATTR];

        // TODO: add ability to load Attractors from csv file
        // TODO: make that csv file

        status = ManagerStatus.Started;
    }

    // should be called by VRPlayerController when placing a new Attractor
    public void addAttr(Attractor attr) {
        attr.transform.SetParent(this.transform);
        attrList.Add(attr);
        n_attr++;
    }
    // should be called whenever an Attractor is deleted by AttractorZone or in a collision
    public void removeAttr(Attractor attr) {
        if (attrList.Remove(attr)) {
            n_attr--;
        }
        else {
            Debug.LogError("SimulationManager.removeAttr: could not find Attractor: " + attr.ToString());
        }
        
    }

    private void FixedUpdate() {
        
        // TODO: update ForceMatrix

        for (int i = 0; i < n_attr; i++) {

            // on each row, only go up to the main diagonal
            for (int j = 0; j <= i; j++) {


            }
        }

        // TODO: apply forces to Attractors

    }



}
