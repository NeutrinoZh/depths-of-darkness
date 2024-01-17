using UnityEngine;

namespace DD.Game {
    public class PlayerInput : MonoBehaviour {
        public Input.PlayerInput Input { get; private set; }

        private void Awake() {
            Input = new();
            Input.Enable();
        }

        private void OnDestroy() {
            Input.Disable();
        }
    }
}