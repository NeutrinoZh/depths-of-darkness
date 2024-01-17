
using System;

using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace DD.Game {
    public sealed class PickController : MonoBehaviour {

        //=====================================================//
        // Events 
        public Action<Pickable> OnPickEvent = null;

        //=====================================================//
        // Props

        public FinderNearPickables NearPickables { get; private set; } = null;

        //=====================================================//
        // Members 

        private PlayerInput m_input = null;

        //============================================================//
        // Lifecycle 

        [Inject]
        public void Consturct(PickablesRegister _register) {
            NearPickables = new FinderNearPickables(transform, _register);
        }

        private void Awake() {
            m_input = GetComponent<PlayerInput>();
            Assert.AreNotEqual(m_input, null);
        }

        private void Update() {
            NearPickables.Find();
        }

        private void Start() {
            m_input.Input.Player.Pick.performed += _ => HandActionHandle();
        }

        private void OnDestroy() {
            m_input.Input.Player.Pick.performed -= _ => HandActionHandle();
        }

        //============================================================//
        // Handles 

        private void HandActionHandle() {
            OnPickEvent?.Invoke(NearPickables.Nearest);
            NearPickables.Find();
        }

        //============================================================//
    }
}