
using System;

using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace DD.Game {
    public sealed class PickController : MonoBehaviour, ILifecycleListener {
        //============================================================//

        private FinderNearPickables m_finderNearPickables = null;

        [Inject]
        public void Consturct(PickablesRegister _register) {
            m_finderNearPickables = new FinderNearPickables(transform, _register);
        }

        //============================================================//

        private PlayerInput m_input = null;

        void ILifecycleListener.OnInit() {
            m_input = GetComponent<PlayerInput>();
            Assert.AreNotEqual(m_input, null);
        }

        //============================================================//

        public FinderNearPickables NearPickables => m_finderNearPickables;

        //============================================================//

        public Action<Pickable> OnPickEvent = null;

        //============================================================//

        void ILifecycleListener.OnUpdate() {
            m_finderNearPickables.Find();
        }

        void ILifecycleListener.OnStart() {
            m_input.Input.Player.Pick.performed += _ => HandActionHandle();
        }

        void ILifecycleListener.OnFinish() {
            m_input.Input.Player.Pick.performed -= _ => HandActionHandle();
        }

        private void HandActionHandle() {
            OnPickEvent?.Invoke(NearPickables.Nearest);
            m_finderNearPickables.Find();
        }

        //============================================================//
    }
}