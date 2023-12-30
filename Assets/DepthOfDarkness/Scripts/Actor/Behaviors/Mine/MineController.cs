using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace DD.Game {
    public class MineController : MonoBehaviour, ILifecycleListener {
        //=======================================//
        
        // internal members 
        const string mMineableTag = "Mineable";

        // dependencies
        private PickablesRegister mPickablesRegister;   
        private GameObservable mGameObservable;

        private PickController mPickController;
        private PlayerState mPlayerState;

        // construct, di 
        
        [Inject]
        public void Construct(GameObservable _gameObservable, PickablesRegister _register) {
            mGameObservable = _gameObservable;
            mPickablesRegister = _register;
        }

        void ILifecycleListener.OnInit() {
            mPickController = GetComponent<PickController>();
            Assert.AreNotEqual(mPickController, null);

            mPlayerState = GetComponent<PlayerState>();
            Assert.AreNotEqual(mPlayerState, null);
        }

        //=======================================//

        void ILifecycleListener.OnStart() {
            mPickController.OnPickEvent += PickHandle;
        }

        void ILifecycleListener.OnFinish() {
            mPickController.OnPickEvent -= PickHandle;
        }

        private void PickHandle(Pickable _pickable) {
            if (!_pickable || !_pickable.CompareTag(mMineableTag))
                return;

            Mine(_pickable);
        }

        private void Mine(Pickable _pickable) {
            mPickablesRegister.RemovePickable(_pickable);
            mGameObservable.DestroyInstance(_pickable.transform);
            mPlayerState.OreCount += 1;
        }
    }
}