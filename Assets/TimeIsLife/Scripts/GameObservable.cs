using System.Collections.Generic;
using UnityEngine;

namespace TIL {
    public class GameObservable : MonoBehaviour {
        private readonly List<ILifecycleListener> mListeners = new();

        // MonoBehavior Start event 
        private void Start() {
            foreach (var listener in mListeners)
                listener.OnStartGame();
        }

        // MonoBehavior Update event 
        private void Update() {
            foreach (var listener in mListeners)
                listener.OnUpdateGame();
        }

        public void AddListener(ILifecycleListener listener) {
            mListeners.Add(listener);
        }
    }
}