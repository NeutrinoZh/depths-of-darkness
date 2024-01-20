using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace DD.Game {
    public class TrolleyController : MonoBehaviour {
        //=======================================//
        // Consts
        const string mTrolleyTag = "Trolley";

        //=======================================//
        // dependencies
        private PickController m_pickController;
        private PlayerState m_playerState;

        //=======================================//
        // Lifecycles 

        private void Awake() {
            //
            m_pickController = GetComponent<PickController>();
            Assert.AreNotEqual(m_pickController, null);

            m_playerState = GetComponent<PlayerState>();
            Assert.AreNotEqual(m_playerState, null);

            //
            m_pickController.OnPickEvent += PickHandle;
        }

        private void OnDestroy() {
            m_pickController.OnPickEvent -= PickHandle;
        }

        //=======================================//
        // Handles 

        private void PickHandle(Pickable _pickable) {
            if (!_pickable || !_pickable.CompareTag(mTrolleyTag))
                return;

            if (!_pickable.TryGetComponent(out TrolleyState trolley))
                return;

            Pour(trolley);
        }

        //=======================================//
        // Internal

        private void Pour(TrolleyState _trolley) {
            _trolley.OreCount += m_playerState.OreCount;
            m_playerState.OreCount = 0;
        }
    }
}