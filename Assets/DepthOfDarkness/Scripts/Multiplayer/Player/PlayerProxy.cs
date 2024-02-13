using System;

using Unity.Netcode;

using UnityEngine;

using Zenject;

namespace DD.Game {
    public class PlayerProxy : NetworkBehaviour {
        //============================================//
        // Events 
        public event Action<Transform> OnPlayerConnected = null;
        public event Action<Transform> OnSelfConnected = null;

        //============================================//
        // Members 
        [SerializeField] private GameObject m_playerPrefab;
        private Transform m_playersParent;

        //============================================//
        // Lifecycle

        [Inject]
        public void Construct(GroupManager _groupManager) {
            m_playersParent = _groupManager.Players;
        }

        private void Start() {
            CreatePlayerServerRpc(NetworkManager.Singleton.LocalClientId);
        }

        //============================================//
        // Internal logic

        [ServerRpc(RequireOwnership = false)]
        private void CreatePlayerServerRpc(ulong _clientId) {
            var player = Instantiate(
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

            if (_clientId == NetworkManager.Singleton.LocalClientId)
                OnSelfConnected?.Invoke(player.transform);
        }
    }
}