using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DD {
    public enum SceneList {
        MAIN_MENU,
        GAME
    }
    
    public sealed class SceneManagement {
        private const string LOADING_SCREEN = "LOADING_SCREEN";

        private bool mLoading;
        private SceneList mToLoadScene;

        private List<IPreloadService> mPreloadServices = new();

        public GameObservable GameObservable { get; set; }

        public void LoadScene(SceneList _scene) {
            if (mLoading)
                return;
            mLoading = true;
            mToLoadScene = _scene;
                
          //  NetworkManager.Singleton.SceneManager.LoadScene(LOADING_SCREEN, LoadSceneMode.Additive);
          //  NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoadingScreenLoaded;
        
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
            if (_clientId != 0)
                return;
            
            foreach (var service in mPreloadServices)
                service.Execute();

            mLoading = false;
            NetworkManager.Singleton.SceneManager.UnloadScene(SceneManager.GetSceneByName(LOADING_SCREEN));
            
            if (GameObservable != null)
                GameObservable.Run();
        } 

        // Preload Service Management  

        public void AddPreloadService(IPreloadService _service) {
            mPreloadServices.Add(_service);
        }
    }
}