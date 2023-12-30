using System;
using UnityEngine;

namespace DD.Game {
    public class TrolleyState : MonoBehaviour, ILifecycleListener {
        [SerializeField] private int mOreCount = 0;

        public int OreCount {
            get => mOreCount;
            set {
                mOreCount = value;
                OnChangeOreCount?.Invoke();
            }
        }

        public Action OnChangeOreCount = null;
    }
}