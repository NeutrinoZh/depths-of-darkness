using UnityEngine;

namespace DD.Game {
    public class PlayerModel {
        private Transform mTransform;

        public PlayerModel(Transform _transform) {
            mTransform = _transform;
        }

        // 
        public Transform Transform => mTransform;

        // consts
        public float MoveSpeed => 2.5f;
    }
}