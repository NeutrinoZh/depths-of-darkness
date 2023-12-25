using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DD.LoadingScreen {
    public class LoadingScreenController : MonoBehaviour, ILifecycleListener {
        private SceneManagement mSceneManagement;

        [Inject]
        public void Construct(SceneManagement _sceneManagement) {
            mSceneManagement = _sceneManagement;
        }

        private float mLoadingDelay = 0f;

        //  LifeCycle

        void ILifecycleListener.OnStart() {
            mSceneManagement.Middleware += SceneLoadingMiddleware;
        }

        void ILifecycleListener.OnFinish() {
            mSceneManagement.Middleware -= SceneLoadingMiddleware;
        }

        //

        private void SceneLoadingMiddleware(Action _continue) {
            StartCoroutine(ContinueDelayCoroutine(_continue));
        }

        private IEnumerator ContinueDelayCoroutine(Action _continue) {
            yield return new WaitForSeconds(mLoadingDelay);
            _continue?.Invoke();
        }
    }
}