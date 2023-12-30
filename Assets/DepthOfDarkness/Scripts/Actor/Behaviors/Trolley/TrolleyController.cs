using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace DD.Game {
    public class TrolleyController : MonoBehaviour, ILifecycleListener {
         //=======================================//
        
        // internal members 
        const string mTrolleyTag = "Trolley";

        // dependencies
        private PickablesRegister mPickablesRegister;   

        private PickController mPickController;
        private PlayerState mPlayerState;

        // construct, di 
        [Inject]
        public void Construct(PickablesRegister _register) {
            mPickablesRegister = _register;
        }

        void ILifecycleListener.OnInit() {
            mPickController = GetComponent<PickController>();
            Assert.AreNotEqual(mPickController, null);

            mPlayerState = GetComponent<PlayerState>();
            Assert.AreNotEqual(mPlayerState, null);
        }

         void ILifecycleListener.OnStart() {
            mPickController.OnPickEvent += PickHandle;
        }

        void ILifecycleListener.OnFinish() {
            mPickController.OnPickEvent -= PickHandle;
        }

        private void PickHandle(Pickable _pickable) {
            if (!_pickable || !_pickable.CompareTag(mTrolleyTag))
                return;

            if (!_pickable.TryGetComponent(out TrolleyState trolley))
                return;

            Pour(trolley);
        }

        private void Pour(TrolleyState _trolley) {
            _trolley.OreCount += mPlayerState.OreCount;
            mPlayerState.OreCount = 0;
        }
    }
}