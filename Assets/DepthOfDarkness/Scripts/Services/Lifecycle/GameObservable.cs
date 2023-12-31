using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DD {
    public sealed class GameObservable : MonoBehaviour {
        private bool mIsStarted = false;
        
        private List<ILifecycleListener> mListeners = new();
        private Queue<ILifecycleListener> mAddingQueue = new();
        private Queue<ILifecycleListener> mRemoveQueue = new();

        private DiContainer mDiContainer;

        [Inject]
        public void Construct(DiContainer _diContainer) {
            mDiContainer = _diContainer;
        }


        public void AddListener(ILifecycleListener listener) {
            if (!mIsStarted) {
                mListeners.Add(listener);
                return;
            }

            mAddingQueue.Enqueue(listener);
        }

        public void RemoveListener(ILifecycleListener listener) {
            if (!mListeners.Contains(listener))
                return;

            if (!mIsStarted) {
                mListeners.Remove(listener);
                return;
            }

            mRemoveQueue.Enqueue(listener);
        }

        public Transform CreateInstance(Transform _transform, Vector3 _position, Quaternion _rotation, Transform _parent) {
            var clone = mDiContainer.InstantiatePrefab(
                _transform.gameObject,
                _position,
                _rotation,
                _parent
            );

            foreach (var behavior in clone.GetComponents<MonoBehaviour>())
                if (behavior is ILifecycleListener listener)
                    AddListener(listener);

            return clone.transform;
        }

        public void DestroyInstance(Transform _transform) {
            foreach (var behavior in _transform.GetComponents<MonoBehaviour>())
                if (behavior is ILifecycleListener listener) {
                    listener.OnFinish();
                    RemoveListener(listener);
                }
            
            Destroy(_transform.gameObject);
        } 

        // ========================================================//
        
        public void Run() {
            mIsStarted = true;
            
            foreach (var listener in mListeners)
                listener.OnInit();

            foreach (var listener in mListeners)
                listener.OnStart();
            
            StartCoroutine(OnFixedCorutine());
        }

        private void Update() {
            if (!mIsStarted)
                return;

            while (mRemoveQueue.TryDequeue(out var listener)) {
                listener.OnFinish();
                mListeners.Remove(listener);
            }

            while (mAddingQueue.TryDequeue(out var listener)) {
                listener.OnStart();
                mListeners.Add(listener);
            }

            foreach (var listener in mListeners)
                listener.OnUpdate();
        }

        private void OnDestroy() {
            if (!mIsStarted)
                return;

            foreach (var listener in mListeners)
                listener.OnFinish();

            mIsStarted = false;
        }

        // ========================================================//

        private IEnumerator OnFixedCorutine() {
            while (true) {
                foreach (var listener in mListeners)
                    listener.OnFixed();
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
    }
}