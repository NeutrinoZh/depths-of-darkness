using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TIL {
    [RequireComponent(typeof(GameObservable))]
    public sealed class LifecycleListenerList : MonoBehaviour {
        private List<MonoBehaviour> listeners;

        private void Awake() {
            listeners = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
            var observable = GetComponent<GameObservable>();            
            foreach (MonoBehaviour behaviour in listeners)
                if (behaviour is ILifecycleListener listener)
                    observable.AddListener(listener);
        }
    }
}