using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement {
    public class ShooterScene : GameScene {
        [SerializeField]
        private ARShooter.GameManager _gameManager;

        public override void SetState(SceneState st) {
            if (st != null) {
                var state = st as GameState;
                _state.UpdateState(state);

                //mSceneObject.transform.position = _state.position;
                //mSceneObject.transform.rotation = _state.rotation;
                //mSceneObject.transform.localScale = _state.scale;

                _gameManager.ARCamera = Camera.main;
                _gameManager.StartGame();
            }
        }

        private void Update() {
            //mSceneObject = this.transform;
            //var distance = Vector3.Distance(Camera.main.transform.position, transform.position);
            ////this.DebugLog("distance to game: " + distance.ToString());
            //if (distance > 5)
            //    doCancel();
        }
    }
}
