using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace DD.Game {
    public class Pickable : MonoBehaviour {
        // ==========================================================//
        // Props 

        public SpriteRenderer Renderer => m_spriteRenderer;
        public Shader DefaultShader => m_defaultShader;
        public int DefaultSpriteOrder => m_defaultSpriteOrder;

        // ==========================================================//
        // Members 

        private PickablesRegister m_pickableRegister;

        private SpriteRenderer m_spriteRenderer;
        private Shader m_defaultShader;
        private int m_defaultSpriteOrder;

        // ==========================================================//
        // Lifecycle 

        [Inject]
        public void Construct(PickablesRegister _register) {
            m_pickableRegister = _register;
        }


        private void Awake() {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            Assert.AreNotEqual(m_spriteRenderer, null);

            m_defaultShader = m_spriteRenderer.material.shader;
            m_defaultSpriteOrder = m_spriteRenderer.sortingOrder;

            m_pickableRegister.AddPickable(this);
        }

        private void OnDestroy() {
            m_pickableRegister.RemovePickable(this);
        }
    }
}