using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    public class PlayerState : IEntityState {

        private Player mPlayer = null;

        // components 
        private Transform mTransform = null;
        private Rigidbody2D mRd = null;

        // states 
        private Direction mDirection = Direction.DOWN;

        private bool mIsMove = false;

        public PlayerState(Player _player, Transform _transform) {
            mPlayer = _player;
            Assert.AreNotEqual(mPlayer, null);

            mTransform = _transform;
            Assert.AreNotEqual(mTransform, null);

            mRd = _transform.GetComponent<Rigidbody2D>();
            Assert.AreNotEqual(mRd, null);
        }

        // props 
        public Direction Direction {
            get => mDirection;
            set {
                if (mDirection == value)
                    return;

                mDirection = value;
                mPlayer.OnDirectionChangeEvent?.Invoke();
            }
        }

        public bool IsMove {
            get => mIsMove;
            set {
                if (mIsMove == value)
                    return;

                mIsMove = value;
                mPlayer.OnStateChangeEvent?.Invoke();
            }
        }

        // readonly 
        public Transform Transform => mTransform;

        public Rigidbody2D Rd => mRd;

        public bool IsStay => !IsMove;

        // consts
        public float MoveSpeed => 2.5f;
    }
}