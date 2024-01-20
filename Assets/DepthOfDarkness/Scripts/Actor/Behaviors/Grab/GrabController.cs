using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace DD.Game {
    public class GrabController : MonoBehaviour {
        //=======================================//
        // Props 

        public Pickable Grabbed { get; private set; } = null;

        //=======================================//
        // Consts 

        const string c_grabbableTag = "Grabbable";
        const string c_grabPoint = "GrabPoint";
        const int c_pickedSpriteOrder = 3;

        //=======================================//
        // Dependencies

        private PickController m_pickController;
        private PickablesRegister m_pickablesRegister;
        private Transform m_grabPoint;

        //=======================================//
        // Members 

        private readonly Vector3 m_grabbedScale = new(0.5f, 0.5f, 1);

        //=======================================//
        // Lifecycles 

        [Inject]
        public void Construct(PickablesRegister _register) {
            m_pickablesRegister = _register;
        }

        private void Awake() {
            m_pickController = GetComponent<PickController>();
            Assert.AreNotEqual(m_pickController, null);

            m_grabPoint = transform.Find(c_grabPoint);
            Assert.AreNotEqual(m_grabPoint, null);
        }

        private void Update() {
            m_pickController.OnPickEvent += PickHandle;
        }

        private void OnDestroy() {
            m_pickController.OnPickEvent -= PickHandle;
        }

        //=======================================//
        // Handles 

        private void PickHandle(Pickable _pickable) {
            if (_pickable && !_pickable.CompareTag(c_grabbableTag))
                return;

            if (!Grabbed)
                Pick(_pickable);
            else
                Drop();
        }

        //=======================================//
        // Internal 

        private void Pick(Pickable _pickable) {
            if (!_pickable)
                return;

            Grabbed = _pickable;

            // 
            m_pickablesRegister.RemovePickable(Grabbed);
            Grabbed.Renderer.sortingOrder = c_pickedSpriteOrder;
            Grabbed.Renderer.material.shader = Grabbed.DefaultShader;

            //
            Grabbed.transform.parent = m_grabPoint;
            Grabbed.transform.localPosition = Vector3.zero;
            Grabbed.transform.localScale = m_grabbedScale;
        }

        private void Drop() {
            if (!Grabbed)
                return;

            //
            m_pickablesRegister.AddPickable(Grabbed);
            Grabbed.Renderer.sortingOrder = Grabbed.DefaultSpriteOrder;

            //
            Grabbed.transform.parent = null;
            Grabbed.transform.localScale = Vector3.one;

            Grabbed = null;
        }
    }
}