using Unity.Netcode;

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

        //public override void OnNetworkSpawn() {
        private void Start() {
            m_pickController.OnPickEvent += PickHandle;
        }

        //public override void OnNetworkDespawn() {
        private void OnDestroy() {
            m_pickController.OnPickEvent -= PickHandle;
        }

        //=======================================//
        // Handles 

        private void PickHandle(Pickable _pickable) {
            if (!_pickable || !_pickable.CompareTag(c_mineableTag))
                return;

            Mine(_pickable);
        }

        //=======================================//
        // Mine

        private void Mine(Pickable _pickable) {
            m_pickablesRegister.RemovePickable(_pickable);
            Destroy(_pickable.gameObject);

            m_playerState.OreCount += 1;
        }

        /*[ServerRpc]
        private void MineServerRpc(ulong _networkId) {
            m_playerState.OreCount += 1;
            MineClientRpc(_networkId);
        }

        [ClientRpc]
        private void MineClientRpc(ulong _networkId) {
            var networkObject = GetNetworkObject(_networkId);
            if (!networkObject)
                return;

            if (!networkObject.TryGetComponent(out Pickable pickable))
                return;

            m_pickablesRegister.RemovePickable(pickable);
            networkObject.Despawn();
        }*/

        //=======================================//
    }
}