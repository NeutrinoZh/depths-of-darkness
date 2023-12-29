using UnityEngine;

namespace DD.Game {
    public class PlayerInput : MonoBehaviour, ILifecycleListener {
        private Input.PlayerInput mInput;
        public Input.PlayerInput Input => mInput;

        void ILifecycleListener.OnInit() {
            mInput = new();
        }

        void ILifecycleListener.OnStart() {
            mInput.Enable();
        }

        void ILifecycleListener.OnFinish() {
            mInput.Disable();
        }
    }
}