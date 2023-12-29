using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    public sealed class MovementController : MonoBehaviour, ILifecycleListener {
        private MovementState mState;
        public MovementState State => mState;

        private Rigidbody2D mRd;
        private PlayerInput mInput;

        void ILifecycleListener.OnInit() {
            mState = new MovementState();
        }

        void ILifecycleListener.OnStart() {
            mInput = new PlayerInput();
            mInput.Enable();

            mRd = GetComponent<Rigidbody2D>();
            Assert.AreNotEqual(mRd, null);
        }

        void ILifecycleListener.OnFinish() {
            mInput.Disable();
        }

        void ILifecycleListener.OnUpdate() {
            // read input value
            Vector2 direction = mInput.Player.Move.ReadValue<Vector2>();

            // move
            Move(direction);

            // switch states
            mState.IsMove = direction != Vector2.zero;
            if (mState.IsMove)
                mState.Direction = DirectionUtils.GetDirectionFromVector(direction);
        }

        private void Move(Vector2 _direction) {
            Vector3 velocity =
                _direction * mState.MoveSpeed;            
            mRd.velocity = velocity;
        }
    }
}