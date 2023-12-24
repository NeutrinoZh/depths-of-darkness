using UnityEngine;

namespace DD.Game {
    public class PlayerController : ILifecycleListener {
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
            Vector2 direction = mInput.Player.Move.ReadValue<Vector2>();
            Move(direction);
        }

        private void Move(Vector2 _direction) {
            Vector3 velocity =
                _direction * mModel.MoveSpeed;
            
            mModel.Rd.velocity = velocity;

            if (_direction != Vector2.zero)
                mModel.Direction = DirectionUtils.GetDirectionFromVector(velocity);
        }
    }
}