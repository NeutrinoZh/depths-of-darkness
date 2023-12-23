using UnityEngine;
using Zenject;

namespace DD.Game {
    public class Player : MonoBehaviour, ILifecycleListener {
        [Inject] private GameObservable gameObservable;
        
        private PlayerModel mModel;
        private PlayerController mController;
        
        void ILifecycleListener.OnStart() {
            mModel = new PlayerModel(transform);
            mController = new PlayerController(this, mModel);

            gameObservable.AddListener(mController);
        }

        void ILifecycleListener.OnFinish() {
            gameObservable.RemoveListener(mController);
        }

        public PlayerModel Model => mModel;
    }
}