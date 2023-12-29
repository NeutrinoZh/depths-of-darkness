
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Zenject;

namespace DD.Game {
    public sealed class PickController : MonoBehaviour, ILifecycleListener {
        //============================================================//

        private PickablesRegister mPickablesRegister = null;
        private FinderNearPickables mFinderNearPickables = null;

        [Inject]
        public void Consturct(PickablesRegister _register) {
            mPickablesRegister = _register;
            mFinderNearPickables = new FinderNearPickables(transform, _register);
        }

        //============================================================//

        private PlayerInput mInput = null;
        private Transform mPickPoint = null;

        void ILifecycleListener.OnInit() {
            mInput = GetComponent<PlayerInput>();
            Assert.AreNotEqual(mInput, null);

            mPickPoint = transform.Find("PickPoint");
            Assert.AreNotEqual(mPickPoint, null);
        }

        //============================================================//

        const int mPickedSpriteOrder = 3;

        private Pickable mGrabbed = null;

        //============================================================//

        public FinderNearPickables NearPickables => mFinderNearPickables;
        
        //============================================================//

        void ILifecycleListener.OnFixed() {
            mFinderNearPickables.Find();
        }

        //============================================================//

        void ILifecycleListener.OnStart() {
            mInput.Input.Player.Pick.performed += _ => Pick();
        }

        void ILifecycleListener.OnFinish() {
            mInput.Input.Player.Pick.performed -= _ => Pick();
        }

        private void Pick() {
            if (mGrabbed || !mFinderNearPickables.Nearest)
                return;

            mGrabbed = mFinderNearPickables.Nearest;

            // 
            mPickablesRegister.RemovePickable(mGrabbed);
            mGrabbed.Renderer.sortingOrder = mPickedSpriteOrder;
            mGrabbed.Renderer.material.shader = mGrabbed.DefaultShader;

            //
            mGrabbed.transform.parent = mPickPoint;
            mGrabbed.transform.localPosition = Vector3.zero;
            mGrabbed.transform.localScale = new(0.8f, 0.8f, 1);

            mFinderNearPickables.Find();
        }
    }
}