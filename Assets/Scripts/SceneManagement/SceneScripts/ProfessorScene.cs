using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneManagement;

public class ProfessorScene : MonoBehaviour, ICallableScene {
    public SceneState GetState() {
        throw new System.NotImplementedException();
    }

    public void ReturnFromSceneWithState(SceneState st) {
        throw new System.NotImplementedException();
    }

    public void SetState(SceneState st) {
        var state = st as ProfessorState;
    }
}
