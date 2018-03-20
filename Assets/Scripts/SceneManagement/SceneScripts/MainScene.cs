using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneManagement;

public class MainScene : MonoBehaviour, ICallableScene {
    public void openHub() {
        FindObjectOfType<ScenesManager>().RunScene(ProjectScenes.hub);
    }

    public void openRoom() {
        FindObjectOfType<ScenesManager>().RunScene(ProjectScenes.corridor);
    }

    public SceneState GetState() {
        return new MainState();
    }

    public void ReturnFromSceneWithState(SceneState st) {
        
    }

    public void SetState(SceneState st) {
        var state = st as MainState;
    }
}
