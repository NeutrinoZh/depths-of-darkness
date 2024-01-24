using Unity.Netcode;

using UnityEngine;

namespace DD.Multiplayer {
    /// <summary>
    /// Start local host if we run game scene via play mode 
    /// </summary>
    public class NetowrkBootstrap : MonoBehaviour {
        private void Awake() {
            // break if network manager already runnig
            if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient)
                return;

            NetworkManager.Singleton.StartHost();
        }
    }
}