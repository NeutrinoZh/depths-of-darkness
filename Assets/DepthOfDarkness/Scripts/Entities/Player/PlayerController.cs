using UnityEngine;

namespace DD.Game {
    public sealed class PlayerController : ILifecycleListener {
        private PlayerModel mModel;
        private Player mPlayer;

        private PlayerInput mInput;

        public PlayerController(Player _player, PlayerModel _model) {
            mPlayer = _player;
            mModel = _model;
        
            mInput = new PlayerInput();
        }

        void ILifecycleListener.OnStart() {
            mInput.Enable();
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
            mModel.IsMove = direction != Vector2.zero;
            if (mModel.IsMove)
                mModel.Direction = DirectionUtils.GetDirectionFromVector(direction);
        }

        private void Move(Vector2 _direction) {
            Vector3 velocity =
                _direction * mModel.MoveSpeed;            
            mModel.Rd.velocity = velocity;
        }
    }
}