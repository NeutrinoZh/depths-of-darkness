using System;
using UnityEngine;
using Zenject;

namespace DD.Game {
    public class Player : MonoBehaviour, ILifecycleListener {
        [SerializeField] private PickConfig mPickConfig; 

        private GameObservable mGameObservable;
        
        private PlayerModel mModel;
        private PlayerController mController;
        private PickController mPickController;
        private PlayerAnimator mAnimator;
        
        [Inject]
        public void Consturct(GameObservable _gameObservable, PickablesRegister _pickableRegister) {
            mGameObservable = _gameObservable;

            mModel = new PlayerModel(this, transform);
            mController = new PlayerController(this, mModel);
            mAnimator = new PlayerAnimator(this, mModel);

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

        public PlayerModel Model => mModel;
        public Action OnDirectionChangeEvent = null;
        public Action OnStateChangeEvent = null;
    }
}