using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRPlayerController : MonoBehaviour, IGameManager {

    public ManagerStatus status { get; private set; }

    public void Startup()
    {
        //TODO
    }

    private void Update() {
        HandleKeyboardInputs();
    }

    private void HandleKeyboardInputs() {
        //TODO
    }

    private void HandleMouseInputs() {
        //TODO
    }

}
