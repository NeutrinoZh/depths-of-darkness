using System;
using UnityEngine;
using Zenject;

namespace DD.Game {
    public class Player : Entity, ILifecycleListener {
        [SerializeField] private PickConfig mPickConfig; 

        private GameObservable mGameObservable;
        
        private PlayerState mState;
        private PlayerController mController;
        private PickController mPickController;
        private PlayerAnimator mAnimator;
        
        [Inject]
        public void Consturct(GameObservable _gameObservable, PickablesRegister _pickableRegister) {
            mGameObservable = _gameObservable;

            mState = new PlayerState(this, transform);
            mController = new PlayerController(this, mState);
            mAnimator = new PlayerAnimator(this, mState);

            mPickController = new PickController(_pickableRegister, transform, mPickConfig);
        }

        void ILifecycleListener.OnStart() {
            mGameObservable.AddListener(mPickController);
            mGameObservable.AddListener(mController);
            mGameObservable.AddListener(mAnimator);
        }

        void ILifecycleListener.OnFinish() {
            mGameObservable.RemoveListener(mPickController);
            mGameObservable.RemoveListener(mController);
            mGameObservable.RemoveListener(mAnimator);
        }

        public PlayerState Model => mState;
        public Action OnDirectionChangeEvent = null;
        public Action OnStateChangeEvent = null;
    }
}