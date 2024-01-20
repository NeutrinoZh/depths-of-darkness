using Unity.Netcode;

using UnityEngine;

namespace DD.Multiplayer {
    public class NetowrkBootstrap : MonoBehaviour {
        private void Awake() {
            if (NetworkManager.Singleton.IsHost)
                return;

            NetworkManager.Singleton.StartHost();
        }
    }
}