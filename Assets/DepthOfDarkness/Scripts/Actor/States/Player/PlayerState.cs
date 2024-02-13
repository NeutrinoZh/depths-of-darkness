using System;

using UnityEngine;

namespace DD.Game {
    public class PlayerState : MonoBehaviour {
        //=======================================//
        // Events 

        public event Action OnChangeOreCount = null;

        //=======================================//
        // Props 

        public int OreCount {
            get => m_oreCount;
            set {
                m_oreCount = value;
                OnChangeOreCount?.Invoke();
            }
        }

        //=======================================//
        // Members 

        [SerializeField] private int m_oreCount = 0;
    }
}