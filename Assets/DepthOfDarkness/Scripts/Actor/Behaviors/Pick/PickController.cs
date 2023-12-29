using UnityEngine;
using Zenject;

namespace DD.Game {
    public sealed class PickController : MonoBehaviour, ILifecycleListener {
        [field: SerializeField] public Shader Highlight { get; private set; }
        private float mPickSqrRadius = 2f;

        private PickablesRegister mPickableRegister = null;
        private Transform mPicker = null; 
        
        [Inject]
        public void Consturct(PickablesRegister _register) {
            mPickableRegister = _register;
            mPicker = transform;
        }

        void ILifecycleListener.OnFixed() {
            var pickables = mPickableRegister.Pickables;

            Pickable nearPickable = null;
            float nearDistance = 0f;

            foreach (var item in pickables) {
                var sqrDistance = (mPicker.position - item.transform.position).sqrMagnitude;

                if (sqrDistance < nearDistance)
                    nearPickable = item;

                item.Renderer.material.shader = 
                    sqrDistance < mPickSqrRadius ? Highlight : item.DefaultShader;
            }

            if (nearDistance > mPickSqrRadius)
                nearPickable = null;
        }
    }
}