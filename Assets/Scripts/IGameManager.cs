using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager {

    ManagerState State { get; set; }

    void Startup();
}
