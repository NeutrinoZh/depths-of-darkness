using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace DD.Game {
    public class PlayerProxy : NetworkBehaviour, ILifecycleListener {
        [SerializeField] private GameObject mPlayerPrefab;        
        private GameObservable mGameObservable;
        private Transform mPlayersParent;

        [Inject]
        public void Construct(GameObservable _gameObservable, WorldManager _worldManager) {
            mGameObservable = _gameObservable;
            mPlayersParent = _worldManager.Players;
        }

        void ILifecycleListener.OnInit() {
            CreatePlayerServerRpc(OwnerClientId);
        }

        [ServerRpc] private void CreatePlayerServerRpc(ulong _clientId) {
            Debug.Log($"Invoked create player: {mGameObservable}, {mPlayersParent}");
            var player = mGameObservable.CreateInstance(
                mPlayerPrefab.transform,
                mPlayersParent.position, mPlayersParent.rotation,
                mPlayersParent
            ).GetComponent<NetworkObject>();
            
            player.SpawnAsPlayerObject(_clientId);
        }
    }
}