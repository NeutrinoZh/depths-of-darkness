using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace DD.Game {
    public class GrabController : MonoBehaviour, ILifecycleListener {
        
        //=======================================//
        
        // internal members 
        const string mGrabbableTag = "Grabbable";
        const int mPickedSpriteOrder = 3;
        private Pickable mGrabbed = null;

        // props 
        public Pickable Grabbed => mGrabbed;

        // dependencies
        private PickController mPickController;
        private PickablesRegister mPickablesRegister;
        private Transform mGrabPoint;

        // construct, di 
        
        [Inject]
        public void Construct(PickablesRegister _register) {
            mPickablesRegister = _register;
        }

        void ILifecycleListener.OnInit() {
            mPickController = GetComponent<PickController>();
            Assert.AreNotEqual(mPickController, null);

            mGrabPoint = transform.Find("GrabPoint");
            Assert.AreNotEqual(mGrabPoint, null);
        }

        //=======================================//

        void ILifecycleListener.OnStart() {
            mPickController.OnPickEvent += PickHandle;
        }

        void ILifecycleListener.OnFinish() {
            mPickController.OnPickEvent -= PickHandle;
        }

        private void PickHandle(Pickable _pickable) {
            if (_pickable && !_pickable.CompareTag(mGrabbableTag))
                return;

            if (!mGrabbed) 
                Pick(_pickable);
            else 
                Drop();
        }

         private void Pick(Pickable _pickable) {
            if (!_pickable)
                return;

            mGrabbed = _pickable;

            // 
            mPickablesRegister.RemovePickable(mGrabbed);
            mGrabbed.Renderer.sortingOrder = mPickedSpriteOrder;
            mGrabbed.Renderer.material.shader = mGrabbed.DefaultShader;

            //
            mGrabbed.transform.parent = mGrabPoint;
            mGrabbed.transform.localPosition = Vector3.zero;
            mGrabbed.transform.localScale = new(0.8f, 0.8f, 1);
        }

        private void Drop() {
            if (!mGrabbed)
                return;

            //
            mPickablesRegister.AddPickable(mGrabbed);
            mGrabbed.Renderer.sortingOrder = mGrabbed.DefaultSpriteOrder;
            
            //
            mGrabbed.transform.parent = null;
            mGrabbed.transform.localScale = new(1, 1, 1);

            mGrabbed = null;
        }
    }
}