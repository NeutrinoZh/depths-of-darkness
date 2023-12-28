using UnityEngine;
using Zenject;

namespace DD.Game {
    public sealed class PickController : IBehavior {
        private PickablesRegister mPickableRegister = null;

        private Transform mPicker = null; 
        private PickConfig mConfig = null;

        public PickController(PickablesRegister _register, Transform _picker, PickConfig _config) {
            mPickableRegister = _register;

            mPicker = _picker;
            mConfig = _config;
        }

        void ILifecycleListener.OnFixed() {
            var pickables = mPickableRegister.Pickables;

            IPickable nearPickable = null;
            float nearDistance = 0f;

            foreach (var item in pickables) {
                var sqrDistance = (item.GetTransform().position - mPicker.position).sqrMagnitude;

                if (sqrDistance < nearDistance)
                    nearPickable = item;

                item.GetRenderer().material.shader = 
                    sqrDistance < mConfig.PickRadius ? mConfig.Highlight : item.GetDefaultShader();
            }

            if (nearDistance > mConfig.PickRadius)
                nearPickable = null;
        }
    }
}