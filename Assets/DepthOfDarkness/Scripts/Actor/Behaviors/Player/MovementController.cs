using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInput))]
    public sealed class MovementController : MonoBehaviour, ILifecycleListener {
        private MovementState mState;
        public MovementState State => mState;

        private Rigidbody2D mRd;
        private PlayerInput mInput;

        void ILifecycleListener.OnInit() {
            mState = new MovementState();
        }

        void ILifecycleListener.OnStart() {
            mRd = GetComponent<Rigidbody2D>();
            Assert.AreNotEqual(mRd, null);

            mInput = GetComponent<PlayerInput>();
            Assert.AreNotEqual(mInput, null);
        }

        void ILifecycleListener.OnUpdate() {
            // read input value
            Vector2 direction = mInput.Input.Player.Move.ReadValue<Vector2>();

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