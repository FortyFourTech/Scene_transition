using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement {
    public class FAGameState : GameState {
        public Vector3 finishPosition;

        public FAGameState() { }

        public FAGameState(Transform tr, Transform finishTr) : base(tr) {
            finishPosition = finishTr.position - tr.position;
            callerButton = finishTr;
        }

        public void UpdateState(FAGameState another) {
            base.UpdateState(another);
            finishPosition = another.finishPosition;
        }
    }
}
