
using System;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace DD.Game {
    public sealed class PickController : MonoBehaviour, ILifecycleListener {
        //============================================================//

        private FinderNearPickables mFinderNearPickables = null;

        [Inject]
        public void Consturct(PickablesRegister _register) {
            mFinderNearPickables = new FinderNearPickables(transform, _register);
        }

        //============================================================//

        private PlayerInput mInput = null;

        void ILifecycleListener.OnInit() {
            mInput = GetComponent<PlayerInput>();
            Assert.AreNotEqual(mInput, null);
        }

        //============================================================//

        public FinderNearPickables NearPickables => mFinderNearPickables;

        //============================================================//

        public Action<Pickable> OnPickEvent = null;
        
        //============================================================//

        void ILifecycleListener.OnFixed() {
            mFinderNearPickables.Find();
        }

        //============================================================//

        void ILifecycleListener.OnStart() {
            mInput.Input.Player.Pick.performed += _ => HandActionHandle();
        }

        void ILifecycleListener.OnFinish() {
            mInput.Input.Player.Pick.performed -= _ => HandActionHandle();
        }

        private void HandActionHandle() {
            OnPickEvent?.Invoke(NearPickables.Nearest);
            mFinderNearPickables.Find();
        }
    }
}