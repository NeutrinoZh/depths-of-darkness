using System;

using Unity.Netcode;

using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace DD.Game {
    public class GrabController : NetworkBehaviour {
        //=======================================//
        // Events

        public event Action<Pickable> OnGrab = null;
        public event Action<Pickable> OnDrop = null;

        //=======================================//
        // Props 

        public Pickable Grabbed { get; private set; } = null;

        //=======================================//
        // Consts 

        const string c_grabbableTag = "Grabbable";

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
            Assert.IsNotNull(m_pickController);
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
                Grab(_pickable);
            else
                Drop();
        }

        //====================//
        // Pick

        private void Grab(Pickable _pickable) {
            if (!_pickable)
                return;

            if (!_pickable.TryGetComponent(out NetworkObject networkObject))
                return;

            GrabServerRpc(networkObject.NetworkObjectId);
        }

        [ServerRpc]
        private void GrabServerRpc(ulong _networkId) {
            var networkObject = GetNetworkObject(_networkId);
            if (!networkObject.TryGetComponent(out Pickable pickable))
                return;

            //
            pickable.transform.parent = transform;
            pickable.transform.localPosition = Vector3.zero;
            pickable.transform.localScale = m_grabbedScale;

            //
            GrabClientRpc(_networkId);
        }

        [ClientRpc]
        private void GrabClientRpc(ulong _networkId) {
            var networkObject = GetNetworkObject(_networkId);
            if (!networkObject.TryGetComponent(out Pickable pickable))
                return;

            m_pickablesRegister.RemovePickable(pickable);

            Grabbed = pickable;
            Grabbed.Renderer.enabled = false;

            OnGrab?.Invoke(pickable);
        }


        //====================//
        // Drop

        private void Drop() {
            if (!Grabbed)
                return;

            DropServerRpc();
        }

        [ServerRpc]
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

            var tmp = Grabbed;

            m_pickablesRegister.AddPickable(Grabbed);
            Grabbed.Renderer.enabled = true;
            Grabbed = null;

            OnDrop?.Invoke(tmp);
        }
    }
}