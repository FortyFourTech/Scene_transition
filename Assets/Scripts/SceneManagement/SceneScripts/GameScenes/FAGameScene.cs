using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement {
    public class FAGameScene : GameScene {
        // [SerializeField] GameController gameController;

        private new FAGameState _state = new FAGameState();

        public override SceneState GetState() {
            return _state;
        }

        public override void SetState(SceneState st) {
            if (st != null) {
                var state = st as FAGameState;
                _state.UpdateState(state);

                mSceneObject.transform.position = _state.position;
                mSceneObject.transform.rotation = _state.rotation;
                mSceneObject.transform.localScale = _state.scale;

                // setup finish cube
                // gameController.SetupFinishPosition(_state.finishPosition);
            }
        }

        private void Awake() {
            // GameController.NewGameEvent += GameController_NewGameEvent;
        }

        private void OnDestroy() {
            // GameController.NewGameEvent -= GameController_NewGameEvent;
        }

        // private void OnGameEvent(GameEvent obj) {
        //     switch (obj) {
        //         case GameEvent.GAMEOVER:
        //             _state.gameResult = GameResult.failed;
        //             returnToCaller();
        //             break;
        //         case GameEvent.WINGAME:
        //             _state.gameResult = GameResult.succeeded;
        //             returnToCaller();
        //             break;
        //         default: break;
        //     }
        // }
    }
}
