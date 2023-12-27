using System;
using UnityEngine;
using Zenject;

namespace DD.Game {
    public class Player : MonoBehaviour, ILifecycleListener {
        [Inject] private GameObservable gameObservable;
        
        private PlayerModel mModel;
        private PlayerController mController;
        private PlayerAnimator mAnimator;
        
        void ILifecycleListener.OnStart() {
            mModel = new PlayerModel(this, transform);
            mController = new PlayerController(this, mModel);
            mAnimator = new PlayerAnimator(this, mModel);

            gameObservable.AddListener(mController);
            gameObservable.AddListener(mAnimator);
        }

        void ILifecycleListener.OnFinish() {
            gameObservable.RemoveListener(mController);
            gameObservable.RemoveListener(mAnimator);
        }

        public PlayerModel Model => mModel;
        public Action OnDirectionChangeEvent = null;
        public Action OnStateChangeEvent = null;
    }
}