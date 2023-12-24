using System;
using System.Collections.Generic;

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
        private Scene mLastScene;
        private SceneList mToLoadScene;

        private List<IPreloadService> mPreloadServices = new();

        public delegate void MiddlewareOnSceneLoad(Action continueLoading);
        public MiddlewareOnSceneLoad Middleware;

        public GameObservable GameObservable { get; set; }

        public void LoadScene(SceneList _scene) {
            if (mLoading)
                return;
            
            mToLoadScene = _scene;
            mLastScene = SceneManager.GetActiveScene();
            mLoading = true;

            var loadProcess = SceneManager.LoadSceneAsync(LOADING_SCREEN, LoadSceneMode.Additive);
            loadProcess.completed += OnLoadingScreenLoaded;
        }

        private void ContinueLoad() {
            var loadProcess = SceneManager.LoadSceneAsync(mToLoadScene.ToString(), LoadSceneMode.Additive);
            loadProcess.completed += OnSceneLoaded;
        }

        private void OnLoadingScreenLoaded(AsyncOperation _operation) {
            var unloadProcess = SceneManager.UnloadSceneAsync(mLastScene);
            unloadProcess.completed += OnSceneUnloaded;
        }

        private void OnSceneUnloaded(AsyncOperation _operation) {
            if (Middleware != null) {
                Middleware.Invoke(ContinueLoad);
            } else {
                ContinueLoad();
            }
        }

        private void OnSceneLoaded(AsyncOperation _operation) {
            foreach (var service in mPreloadServices)
                service.Execute();

            mLoading = false;
            SceneManager.UnloadSceneAsync(LOADING_SCREEN);
            
            if (GameObservable != null)
                GameObservable.Run();
        } 

        // Preload Service Management  

        public void AddPreloadService(IPreloadService _service) {
            mPreloadServices.Add(_service);
        }
    }
}