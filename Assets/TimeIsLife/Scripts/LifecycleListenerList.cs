using System.Collections.Generic;
using UnityEngine;

namespace TIL {
    [RequireComponent(typeof(GameObservable))]
    public class LifecycleListenerList : MonoBehaviour {
        [SerializeField] private List<MonoBehaviour> listeners;

        private void Awake() {
            var observable = GetComponent<GameObservable>();            
            foreach (MonoBehaviour behaviour in listeners)
                if (behaviour is ILifecycleListener listener)
                    observable.AddListener(listener);
        }
    }
}