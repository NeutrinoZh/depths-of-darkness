using System;
using UnityEngine;

namespace DD.Game {
    public class PlayerState : MonoBehaviour {
        private int mOreCount = 0;
        
        public int OreCount {
            get => mOreCount;
            set {
                mOreCount = value;
                OnChangeOreCountEvent?.Invoke();
            }
        }

        public Action OnChangeOreCountEvent = null;
    }
}