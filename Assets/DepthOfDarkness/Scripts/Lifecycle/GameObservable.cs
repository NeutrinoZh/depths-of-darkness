using System.Collections.Generic;
using UnityEngine;

namespace DD {
    public sealed class GameObservable : MonoBehaviour {
        private bool mIsStarted = false;
        private readonly List<ILifecycleListener> mListeners = new();
        private Queue<ILifecycleListener> mAddingQueue = new();

        // MonoBehavior Start event 
        private void Start() {
            mIsStarted = true;
            foreach (var listener in mListeners)
                listener.OnStartGame();
        }

        // MonoBehavior Update event 
        private void Update() {
            while (mAddingQueue.TryDequeue(out var listener)) {
                listener.OnStartGame();
                mListeners.Add(listener);
            }

            foreach (var listener in mListeners)
                listener.OnUpdateGame();
        }

        private void OnDestroy() {
            foreach (var listener in mListeners)
                listener.OnFinishGame();
        }

        public void AddListener(ILifecycleListener listener) {
            if (!mIsStarted) {
                mListeners.Add(listener);
                return;
            }

            mAddingQueue.Enqueue(listener);
        }
    }
}