using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    [RequireComponent(typeof(PickController))]
    public class OutlingPickables : MonoBehaviour {
        // ==========================================================//

        // Props 
        [field: SerializeField] public Shader Highlight { get; private set; }

        // Members 
        private FinderNearPickables m_finderNearPickables = null;

        // ==========================================================//
        // Lifecycle

        private void Awake() {
            var pickController = GetComponent<PickController>();
            Assert.AreNotEqual(pickController, null);

            m_finderNearPickables = pickController.NearPickables;
            Assert.AreNotEqual(m_finderNearPickables, null);
        }

        private void Update() {
            ChangeShadersOnPickables();
        }

        // ==========================================================//
        // Handlers 

        private void ChangeShadersOnPickables() {
            foreach (var item in m_finderNearPickables.PickablesInRadius)
                if (item != null)
                    item.Renderer.material.shader = Highlight;

            foreach (var item in m_finderNearPickables.PickablesOutRadius) {
                if (item != null)
                    item.Renderer.material.shader = item.DefaultShader;
            }
        }
    }
}