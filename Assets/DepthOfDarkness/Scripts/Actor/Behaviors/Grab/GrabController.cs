using Unity.Netcode;

using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace DD.Game {
    public class GrabController : NetworkBehaviour {
        //=======================================//
        // Props 

        public Pickable Grabbed { get; private set; } = null;

        //=======================================//
        // Consts 

        const string c_grabbableTag = "Grabbable";
        const int c_pickedSpriteOrder = 3;

        //=======================================//
        // Dependencies

        private PickController m_pickController;
        private PickablesRegister m_pickablesRegister;

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
        }

        public override void OnNetworkSpawn() {
            m_pickController.OnPickEvent += PickHandle;
        }

        public override void OnNetworkDespawn() {
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


        //====================//
        // Pick

        private void Pick(Pickable _pickable) {
            if (!_pickable)
                return;

            if (!_pickable.TryGetComponent(out NetworkObject networkObject))
                return;

            PickServerRpc(networkObject.NetworkObjectId);
        }

        [ServerRpc(RequireOwnership = false)]
        private void PickServerRpc(ulong _networkId) {
            var networkObject = GetNetworkObject(_networkId);
            if (!networkObject.TryGetComponent(out Pickable pickable))
                return;

            //
            pickable.transform.parent = transform;
            pickable.transform.localPosition = Vector3.zero;
            pickable.transform.localScale = m_grabbedScale;

            //
            PickClientRpc(_networkId);
        }

        [ClientRpc]
        private void PickClientRpc(ulong _networkId) {
            var networkObject = GetNetworkObject(_networkId);
            if (!networkObject.TryGetComponent(out Pickable pickable))
                return;

            m_pickablesRegister.RemovePickable(pickable);

            Grabbed = pickable;
            Grabbed.Renderer.sortingOrder = c_pickedSpriteOrder;
            Grabbed.Renderer.material.shader = Grabbed.DefaultShader;
        }


        //====================//
        // Drop

        private void Drop() {
            if (!Grabbed)
                return;

            DropServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void DropServerRpc() {
            if (!Grabbed)
                return;

            Grabbed.transform.parent = null;
            Grabbed.transform.localScale = Vector3.one;

            DropClientRpc();
        }

        [ClientRpc]
        private void DropClientRpc() {
            if (!Grabbed)
                return;

            m_pickablesRegister.AddPickable(Grabbed);
            Grabbed.Renderer.sortingOrder = Grabbed.DefaultSpriteOrder;
            Grabbed = null;
        }
    }
}