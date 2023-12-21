using UnityEngine;
using Zenject;

namespace DD.Game {
    public class Player : MonoBehaviour, ILifecycleListener {
        [Inject] private GameObservable gameObservable;
        
        private PlayerModel mModel;
        private PlayerController mController;
        
        public void OnStartGame() {
            mModel = new PlayerModel(transform);
            mController = new PlayerController(this, mModel);

            gameObservable.AddListener(mController);
        }


        public PlayerModel Model => mModel;
    }
}