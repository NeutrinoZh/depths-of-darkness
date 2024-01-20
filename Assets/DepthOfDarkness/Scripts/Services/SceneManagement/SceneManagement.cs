using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace DD {
    public enum SceneList {
        MAIN_MENU,
        GAME
    }

    public sealed class SceneManagement : NetworkBehaviour {
        private const string c_loadingScreen = "LOADING_SCREEN";

        private bool m_loading;
        private SceneList m_toLoadScene;

        private void Awake() {
            DontDestroyOnLoad(this);
        }

        public void LoadScene(SceneList _scene) {
            if (m_loading)
                return;
            m_loading = true;
            m_toLoadScene = _scene;

            var proccess = SceneManager.LoadSceneAsync(c_loadingScreen, LoadSceneMode.Single);
            proccess.completed += OnLoadingScreenLoaded;
        }

        public void Join(string _joinCode) {
            if (m_loading)
                return;
            m_loading = true;

            var proccess = SceneManager.LoadSceneAsync(c_loadingScreen, LoadSceneMode.Single);
            proccess.completed += _ => StartClient(_joinCode);
        }

        private async void StartClient(string _joinCode) {
            await Multiplayer.RelayControll.StartClientWithRelay(_joinCode);
        }

        private async void OnLoadingScreenLoaded(AsyncOperation _) {
            var joinCode = await Multiplayer.RelayControll.StartHostWithRelay();
            Debug.Log(joinCode);

            NetworkManager.Singleton.SceneManager.LoadScene(m_toLoadScene.ToString(), LoadSceneMode.Additive);
            NetworkManager.Singleton.SceneManager.OnLoadComplete += OnSceneLoaded;
        }

        private void OnSceneLoaded(ulong _clientId, string _sceneName, LoadSceneMode _loadSceneMode) {
            if (_clientId == 0)
                SceneManager.UnloadSceneAsync(c_loadingScreen);
            ConnectClientRpc(_clientId);
        }

        [ClientRpc]
        private void ConnectClientRpc(ulong _clientId) {
            if (NetworkManager.Singleton.LocalClientId != _clientId)
                return;

            m_loading = false;
        }
    }
}