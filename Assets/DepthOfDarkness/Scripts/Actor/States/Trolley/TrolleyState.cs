using System;
using UnityEngine;

namespace DD.Game {
    public class TrolleyState : MonoBehaviour, ILifecycleListener {
        private int mOreCount = 0;

        public int OreCount {
            get => mOreCount;
            set {
                mOreCount = value;
                ChangeOreCountHandle?.Invoke();
            }
        }

        public Action ChangeOreCountHandle = null;
    }
}