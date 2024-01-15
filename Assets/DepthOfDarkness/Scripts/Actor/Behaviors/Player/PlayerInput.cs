using UnityEngine;

namespace DD.Game {
    public class PlayerInput : MonoBehaviour, ILifecycleListener {
        private Input.PlayerInput mInput;
        public Input.PlayerInput Input => mInput;

        private void Awake() {
            mInput = new();
            mInput.Enable();
        }

        void ILifecycleListener.OnInit() {

        }

        void ILifecycleListener.OnFinish() {
            mInput.Disable();
        }
    }
}