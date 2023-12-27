using UnityEngine;

namespace DD.Game {
    public sealed class PickController : ILifecycleListener {
        private PickablesRegister mPickableRegister = null;

        private Transform mPicker = null; 
        private Shader mHighlight = null;
        private float mPickRadius = 0;

        public PickController(Transform _picker, PickConfig _config) {
            mPicker = _picker;
            mHighlight = _config.Highlight;
            mPickRadius = _config.PickRadius;
        }

        void ILifecycleListener.OnFixed() {
            var pickables = mPickableRegister.FindAndSortPickablesInRadius(mPicker.position, mPickRadius);
            foreach (var item in pickables)
                item.GetRenderer().material.shader = mHighlight;
        }
    }
}