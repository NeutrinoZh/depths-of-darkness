using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    public class PlayerAnimator : ILifecycleListener {
        private Player mPlayer;
        private PlayerModel mModel;

        private Animator mAnimator;

        public PlayerAnimator(Player _player, PlayerModel _model) {
            mPlayer = _player;
            mModel = _model;
        }

        void ILifecycleListener.OnStart() {
            mAnimator = mModel.Transform.GetComponent<Animator>();
            Assert.AreNotEqual(mAnimator, null);

            mPlayer.OnMoveEvent += OnMoveHandle;
        }

        void ILifecycleListener.OnFinish() {
            mPlayer.OnMoveEvent -= OnMoveHandle;
        }

        private void OnMoveHandle(Direction _direction) {
            mAnimator.Play($"Move{_direction.ToPrettyString()}");
        }
    }
}