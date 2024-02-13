using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace DD.Game {
    public class Pickable : MonoBehaviour {
        // ==========================================================//
        // Props 

        public SpriteRenderer Renderer { get; private set; }
        public Shader DefaultShader { get; private set; }
        public int DefaultSpriteOrder { get; private set; }

        // ==========================================================//
        // Members 

        private PickablesRegister m_pickableRegister;

        // ==========================================================//
        // Lifecycle 

        [Inject]
        public void Construct(PickablesRegister _register) {
            m_pickableRegister = _register;
        }


        private void Awake() {
            Renderer = GetComponent<SpriteRenderer>();
            Assert.IsNotNull(Renderer);

            DefaultShader = Renderer.material.shader;
            DefaultSpriteOrder = Renderer.sortingOrder;

            m_pickableRegister.AddPickable(this);
        }

        private void OnDestroy() {
            m_pickableRegister.RemovePickable(this);
        }
    }
}