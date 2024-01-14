using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace DD.Game {
    public class PlayerProxy : NetworkBehaviour {
        [SerializeField] private GameObject mPlayerPrefab;        
        private GameObservable mGameObservable;
        private Transform mPlayersParent;

        [Inject]
        public void Construct(GameObservable _gameObservable, WorldManager _worldManager) {
            mGameObservable = _gameObservable;
            mPlayersParent = _worldManager.Players;
        }

        private void Start() {
            NetworkManager.Singleton.SceneManager.OnSynchronizeComplete += (ulong _clientId) => {
                CreatePlayerServerRpc();
            };
        }

        [ServerRpc] private void CreatePlayerServerRpc() {
            var player = mGameObservable.CreateInstance(
                mPlayerPrefab.transform,
                mPlayersParent.position, mPlayersParent.rotation,
                mPlayersParent
            ).GetComponent<NetworkObject>();
            
            player.Spawn();
        }
    }
}