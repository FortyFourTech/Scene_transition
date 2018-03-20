using SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubScene : MonoBehaviour, ICallableScene {
    public Transform FAStartPoint;

    private HubState _state = new HubState();

    public void launchLazors(Transform tr) {
        //FindObjectOfType<ScenesManager>().RunScene(ProjectScenes.game1);
        tr.gameObject.SetActive(false); // here launch loading animation

        FindObjectOfType<ScenesManager>().LoadGame (
            HackGame.lazors,
            new GameState(tr)
        );
    }

    public void launchXnb(Transform tr) {
        //FindObjectOfType<ScenesManager>().RunScene(ProjectScenes.game1);
        tr.gameObject.SetActive(false); // here launch loading animation

        FindObjectOfType<ScenesManager>().LoadGame (
            HackGame.xnb,
            new XNBGameState(tr)
        );
    }

    public void launchFa(Transform tr) {
        //FindObjectOfType<ScenesManager>().RunScene(ProjectScenes.game1);
        tr.gameObject.SetActive(false); // here launch loading animation

        FindObjectOfType<ScenesManager>().LoadGame (
            HackGame.freaky_architect,
            new FAGameState(FAStartPoint, tr)
        );
    }

    public void launchRB(Transform tr) {
        //FindObjectOfType<ScenesManager>().RunScene(ProjectScenes.game1);
        tr.gameObject.SetActive(false); // here launch loading animation


        FindObjectOfType<ScenesManager>().LoadGame(
            HackGame.round_ball,
            new RBGameState(tr)
        );
    }

    public void launchShooter(Transform tr) {
        //FindObjectOfType<ScenesManager>().RunScene(ProjectScenes.game1);
        tr.gameObject.SetActive(false); // here launch loading animation

        FindObjectOfType<ScenesManager>().LoadGame(
            HackGame.shooter,
            new GameState(tr)
        );
    }

    public SceneState GetState() {
        return _state;
    }

    public void ReturnFromSceneWithState(SceneState st) {
        this.DebugLog("returned from game with state: " + st.ToString());

        var state = st as GameState;
        if (state != null) {

            Interface.InterfaceManager interfaceManager = FindObjectOfType<Interface.InterfaceManager>();
            //Отправляем интерфейсу результат взлома
            if (interfaceManager != null)
                interfaceManager.SetHackingGameState(state.gameResult);

            if (state.gameResult == GameResult.succeeded)
                this.DebugLog("game succeed");          
            else if (state.gameResult == GameResult.failed)
                this.DebugLog("gameFailed");
            else if (state.gameResult == GameResult.cancelled)
                this.DebugLog("gameCancelled");

            state.callerButton.gameObject.SetActive(true);
        }
    }

    public void SetState(SceneState st) {
        var state = st as HubState;
        _state.UpdateState(state);
    }
}
