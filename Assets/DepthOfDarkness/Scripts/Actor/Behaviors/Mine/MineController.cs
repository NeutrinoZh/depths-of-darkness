using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace DD.Game {
    public class MineController : MonoBehaviour {
        //=======================================//
        // Consts 

        const string c_mineableTag = "Mineable";

        //=======================================//
        // Dependencies

        private PickablesRegister m_pickablesRegister;

        private PickController m_pickController;

        private PlayerState m_playerState;

        //=======================================//
        // Lifecycles 

        [Inject]
        public void Construct(PickablesRegister _register) {
            m_pickablesRegister = _register;
        }

        void Awake() {
            m_pickController = GetComponent<PickController>();
            Assert.AreNotEqual(m_pickController, null);

            m_playerState = GetComponent<PlayerState>();
            Assert.AreNotEqual(m_playerState, null);
        }

        void Start() {
            m_pickController.OnPickEvent += PickHandle;
        }

        void OnDestroy() {
            m_pickController.OnPickEvent -= PickHandle;
        }

        //=======================================//
        // Handles 

        private void PickHandle(Pickable _pickable) {
            if (!_pickable || !_pickable.CompareTag(c_mineableTag))
                return;

            Mine(_pickable);
        }

        private void Mine(Pickable _pickable) {
            m_pickablesRegister.RemovePickable(_pickable);
            m_playerState.OreCount += 1;
        }

        //=======================================//
    }
}