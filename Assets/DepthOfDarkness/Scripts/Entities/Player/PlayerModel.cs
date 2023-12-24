using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    public class PlayerModel {

        // components 
        private Transform mTransform = null;
        private Rigidbody2D mRd = null;

        // states 
        private Direction mDirection = Direction.DOWN;

        private bool mIsMove = false;

        public PlayerModel(Transform _transform) {
            mTransform = _transform;
            Assert.AreNotEqual(mTransform, null);

            mRd = _transform.GetComponent<Rigidbody2D>();
            Assert.AreNotEqual(mRd, null);
        }

        // props 
        public Direction Direction {
            get => mDirection;
            set => mDirection = value;
        }

        public bool IsMove {
            get => mIsMove;
            set => mIsMove = value;
        }

        // readonly 
        public Transform Transform => mTransform;

        public Rigidbody2D Rd => mRd;

        public bool IsStay => !IsMove;

        // consts
        public float MoveSpeed => 2.5f;
    }
}