using System;

using Unity.Netcode;

namespace DD.Game {
    public class TrolleyState : NetworkBehaviour {
        //=======================================//
        // Events 

        public event Action OnChangeOreCount = null;

        //=======================================//
        // Props 

        public int OreCount {
            get => m_oreCount.Value;
            set {
                SetOreCountServerRpc(value);
            }
        }

        //=======================================//
        // Members 

        private readonly NetworkVariable<int> m_oreCount = new NetworkVariable<int>(0);

        //=======================================//
        // Network events 

        private void Awake() {
            m_oreCount.OnValueChanged += (_, _) => OnChangeOreCount?.Invoke();
        }

        //=======================================//
        // Server Rpcs 

        [ServerRpc(RequireOwnership = false)]
        private void SetOreCountServerRpc(int _oreCount) {
            m_oreCount.Value = _oreCount;
        }
    }
}