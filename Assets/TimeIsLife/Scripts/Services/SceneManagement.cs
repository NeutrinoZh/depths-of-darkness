using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TIL {
    public enum SceneList {
        MAIN_MENU,
        GAME
    }
    
    public sealed class SceneManagement {
        private const string LOADING_SCREEN = "LOADING_SCREEN";

        private bool mLoading;
        private Scene mLastScene;
        private SceneList mToLoadScene;

        public delegate void MiddlewareOnSceneLoad(Action continueLoading);
        public MiddlewareOnSceneLoad Middleware;

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
            mLoading = false;
            SceneManager.UnloadSceneAsync(LOADING_SCREEN);
        } 
    }
}