using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour, IGameManager {

    public GameObject planetPrefab;
    public GameObject AttractorsObj;

    private ManagerState _state;

    ManagerState IGameManager.State {
        get {
            return _state;
        }
        set { } // read-only
    }



    private void Awake() {
        _state = ManagerState.NOT_STARTED;
    }

    public void Startup() {
        AttractorsObj = GameObject.Find("Attractors");
        _state = ManagerState.STARTED;
    }

}
