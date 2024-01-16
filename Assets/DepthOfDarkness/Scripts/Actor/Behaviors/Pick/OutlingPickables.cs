using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    [RequireComponent(typeof(PickController))]
    public class OutlingPickables : MonoBehaviour, ILifecycleListener {
        [field: SerializeField] public Shader Highlight { get; private set; }
        private FinderNearPickables mFinderNearPickables = null;

        void ILifecycleListener.OnStart() {
            mFinderNearPickables = GetComponent<PickController>()?.NearPickables;
            Assert.AreNotEqual(mFinderNearPickables, null);
        }

        void ILifecycleListener.OnUpdate() {
            ChangeShadersOnPickables();
        }

        private void ChangeShadersOnPickables() {
            foreach (var item in mFinderNearPickables.PickablesInRadius)
                if (item != null)
                    item.Renderer.material.shader = Highlight;

            foreach (var item in mFinderNearPickables.PickablesOutRadius) {
                if (item != null)
                    item.Renderer.material.shader = item.DefaultShader;
            }
        }
    }
}