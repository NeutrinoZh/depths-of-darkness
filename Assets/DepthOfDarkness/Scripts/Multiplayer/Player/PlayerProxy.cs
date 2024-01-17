using Unity.Netcode;

using UnityEngine;

using Zenject;

namespace DD.Game {
    public class PlayerProxy : NetworkBehaviour {
        [SerializeField] private GameObject m_playerPrefab;
        private Transform m_playersParent;

        [Inject]
        public void Construct(GroupManager _groupManager) {
            m_playersParent = _groupManager.Players;
        }

        private void Awake() {
            CreatePlayerServerRpc(OwnerClientId);
        }

        [ServerRpc]
        private void CreatePlayerServerRpc(ulong _clientId) {
            /*var player = mGameObservable.CreateInstance(
                m_playerPrefab.transform,
                m_playersParent.position, m_playersParent.rotation,
                m_playersParent
            ).GetComponent<NetworkObject>();

            player.SpawnAsPlayerObject(_clientId);*/
        }
    }
}