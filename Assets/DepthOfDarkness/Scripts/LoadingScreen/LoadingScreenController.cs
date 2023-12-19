using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace DD.LoadingScreen {
    public class LoadingScreenController : MonoBehaviour, ILifecycleListener {
        private SceneManagement sceneManagement;
        
        [Inject]
        public void Construct(SceneManagement _sceneManagement) {
            sceneManagement = _sceneManagement;
        }

        private float mLoadingDelay = 2f;

        public void OnStartGame() {
            sceneManagement.Middleware += SceneLoadingMiddleware;
        }

        public void OnFinishGame() {
            sceneManagement.Middleware -= SceneLoadingMiddleware;
        }

        private void SceneLoadingMiddleware(Action _continue) {
            StartCoroutine(ContinueDelayCoroutine(_continue));
        }

        private IEnumerator ContinueDelayCoroutine(Action _continue) {
            yield return new WaitForSeconds(mLoadingDelay);
            _continue?.Invoke();
        }
    }
}