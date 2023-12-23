using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    public class PlayerModel {
        private Transform mTransform;
        private Rigidbody2D mRd;

        public PlayerModel(Transform _transform) {
            mTransform = _transform;
            Assert.AreNotEqual(mTransform, null);

            mRd = _transform.GetComponent<Rigidbody2D>();
            Assert.AreNotEqual(mRd, null);
        }

        // 
        public Transform Transform => mTransform;

        public Rigidbody2D Rd => mRd;

        // consts
        public float MoveSpeed => 2.5f;
    }
}