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

        public void OnStartGame() {
            mInput.Enable();
        }

        public void OnFinishGame() {
            mInput.Disable();
        }

        public void OnUpdateGame() {
            Vector2 direction = mInput.Player.Move.ReadValue<Vector2>();
            Move(direction);
        }

        private void Move(Vector2 _direction) {
            Vector3 shift =
                _direction * mModel.MoveSpeed  * Time.deltaTime;
            
            mModel.Transform.position += shift;
        }
    }
}