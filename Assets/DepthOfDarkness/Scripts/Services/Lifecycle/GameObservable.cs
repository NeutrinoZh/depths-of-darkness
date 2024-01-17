using System.Collections.Generic;

using UnityEngine;

using Zenject;

namespace DD {
    public sealed class GameObservable : MonoBehaviour {
        private bool m_isStarted = false;

        private readonly List<ILifecycleListener> m_listeners = new();
        private readonly Queue<ILifecycleListener> m_addingQueue = new();
        private readonly Queue<ILifecycleListener> m_removeQueue = new();

        private DiContainer m_diContainer;

        [Inject]
        public void Construct(DiContainer _diContainer) {
            m_diContainer = _diContainer;
        }

        public void AddListener(ILifecycleListener _listener) {
            if (!m_isStarted) {
                m_listeners.Add(_listener);
                return;
            }

            m_addingQueue.Enqueue(_listener);
        }

        public void RemoveListener(ILifecycleListener _listener) {
            if (!m_listeners.Contains(_listener))
                return;

            if (!m_isStarted) {
                m_listeners.Remove(_listener);
                return;
            }

            m_removeQueue.Enqueue(_listener);
        }

        // ========================================================//

        public Transform CreateInstance(Transform _transform, Vector3 _position, Quaternion _rotation, Transform _parent) {
            var clone = m_diContainer.InstantiatePrefab(
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
            m_isStarted = true;

            foreach (var listener in m_listeners)
                listener.OnInit();

            foreach (var listener in m_listeners)
                listener.OnStart();
        }

        private void Update() {
            if (!m_isStarted)
                return;

            while (m_removeQueue.TryDequeue(out var listener)) {
                listener.OnFinish();
                m_listeners.Remove(listener);
            }

            while (m_addingQueue.TryDequeue(out var listener)) {
                listener.OnInit();
                listener.OnStart();
                m_listeners.Add(listener);
            }

            foreach (var listener in m_listeners)
                listener.OnUpdate();
        }

        private void OnDestroy() {
            if (!m_isStarted)
                return;

            foreach (var listener in m_listeners)
                listener.OnFinish();

            m_isStarted = false;
        }
    }
}