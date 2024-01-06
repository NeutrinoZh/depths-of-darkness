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
        private const string LOADING_SCREEN = "LOADING_SCREEN";

        private bool mLoading;
        private SceneList mToLoadScene;

        private List<IPreloadService> mPreloadServices = new();

        public GameObservable GameObservable { get; set; }

        private void Awake() {
            DontDestroyOnLoad(this);
        }

        public void LoadScene(SceneList _scene) {
            if (mLoading)
                return;
            mLoading = true;
            mToLoadScene = _scene;
        
            var proccess = SceneManager.LoadSceneAsync(LOADING_SCREEN, LoadSceneMode.Single);
            proccess.completed += OnLoadingScreenLoaded;
        }

        public void Join(string _joinCode) {
            if (mLoading)
                return;
            mLoading = true;

            var proccess = SceneManager.LoadSceneAsync(LOADING_SCREEN, LoadSceneMode.Single);
            proccess.completed += _ => StartClient(_joinCode);
        }

        private async void StartClient(string _joinCode) {
            await Multiplayer.RelayControll.StartClientWithRelay(_joinCode);
        }

        private async void OnLoadingScreenLoaded(AsyncOperation _) {
            var joinCode = await Multiplayer.RelayControll.StartHostWithRelay();
            Debug.Log(joinCode);

            NetworkManager.Singleton.SceneManager.LoadScene(mToLoadScene.ToString(), LoadSceneMode.Additive);
            NetworkManager.Singleton.SceneManager.OnLoadComplete += OnSceneLoaded;
        }

        private void OnSceneLoaded(ulong _clientId, string _sceneName, LoadSceneMode _loadSceneMode) {
            if (_clientId == 0)
                NetworkManager.Singleton.SceneManager.UnloadScene(SceneManager.GetSceneByName(LOADING_SCREEN));
            ConnectClientRpc(_clientId);
        } 

        [ClientRpc] private void ConnectClientRpc(ulong _clientId) {
            if (NetworkManager.Singleton.LocalClientId != _clientId)
                return;

            mLoading = false;

            foreach (var service in mPreloadServices)
                service.Execute();

            if (GameObservable != null)
                GameObservable.Run();
        }

        public void AddPreloadService(IPreloadService _service) {
            mPreloadServices.Add(_service);
        }
    }
}