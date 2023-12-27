using System;
using UnityEngine;
using Zenject;

namespace DD.Game {
    public class Player : MonoBehaviour, ILifecycleListener {
        private GameObservable mGameObservable;
        
        private PlayerModel mModel;
        private PlayerController mController;
        private PlayerAnimator mAnimator;
        
        [Inject]
        public void Consturct(GameObservable _gameObservable) {
            mGameObservable = _gameObservable;
        }

        void ILifecycleListener.OnStart() {
            mModel = new PlayerModel(this, transform);
            mController = new PlayerController(this, mModel);
            mAnimator = new PlayerAnimator(this, mModel);

            mGameObservable.AddListener(mController);
            mGameObservable.AddListener(mAnimator);
        }

        void ILifecycleListener.OnFinish() {
            mGameObservable.RemoveListener(mController);
            mGameObservable.RemoveListener(mAnimator);
        }

        public PlayerModel Model => mModel;
        public Action OnDirectionChangeEvent = null;
        public Action OnStateChangeEvent = null;
    }
}