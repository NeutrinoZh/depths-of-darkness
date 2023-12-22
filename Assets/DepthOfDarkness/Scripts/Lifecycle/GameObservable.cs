using System.Collections.Generic;
using UnityEngine;

namespace DD {
    public sealed class GameObservable : MonoBehaviour {
        private bool mIsStarted = false;
        private List<ILifecycleListener> mListeners = new();
        private Queue<ILifecycleListener> mAddingQueue = new();
        private Queue<ILifecycleListener> mRemoveQueue = new();

        // MonoBehavior Start event 
        private void Start() {
            mIsStarted = true;
            foreach (var listener in mListeners)
                listener.OnStart();
        }

        // MonoBehavior Update event 
        private void Update() {
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
            foreach (var listener in mListeners)
                listener.OnFinish();
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
    }
}