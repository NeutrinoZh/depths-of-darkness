using System;

using Unity.Netcode;

using UnityEngine;

using Zenject;

namespace DD.Game {
    public class PlayerProxy : NetworkBehaviour {
        //============================================//
        // Events 
        public Action<Transform> OnPlayerConnected = null;
        public Action<Transform> OnSelfConnect = null;

        //============================================//
        // Members 
        [SerializeField] private GameObject m_playerPrefab;
        private Transform m_playersParent;
        private DiContainer m_diContainer;

        //============================================//
        // Lifecycle

        [Inject]
        public void Construct(DiContainer _diContainer, GroupManager _groupManager) {
            m_diContainer = _diContainer;
            m_playersParent = _groupManager.Players;
        }

        private void Start() {
            CreatePlayerServerRpc(OwnerClientId);
        }

        //============================================//
        // Internal logic

        [ServerRpc]
        private void CreatePlayerServerRpc(ulong _clientId) {
            var player = m_diContainer.InstantiatePrefab(
                m_playerPrefab,
                m_playersParent.position,
                m_playersParent.rotation,
                m_playersParent
            ).GetComponent<NetworkObject>();

            player.SpawnAsPlayerObject(_clientId);
            PlayerCreatedClientRpc(player.NetworkObjectId, _clientId);
        }

        [ClientRpc]
        void PlayerCreatedClientRpc(ulong _playerId, ulong _clientId) {
            var player = GetNetworkObject(_playerId);
            OnPlayerConnected?.Invoke(player.transform);

            if (_clientId == OwnerClientId)
                OnSelfConnect?.Invoke(player.transform);
        }
    }
}