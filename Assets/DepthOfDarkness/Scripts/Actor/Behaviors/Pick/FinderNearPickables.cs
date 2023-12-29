using System.Collections.Generic;
using UnityEngine;

namespace DD.Game {
    public sealed class FinderNearPickables {

        private PickablesRegister mPickableRegister = null;
        private Transform mPicker = null;

        //==============================================================//

        private const float mPickSqrRadius = 2f;

        private List<Pickable> mPickablesInRadius = new();
        private List<Pickable> mPickablesOutRadius = new();

        private Pickable mNearest = null;

        //==============================================================//

        public Pickable Nearest => mNearest;
        public List<Pickable> PickablesInRadius => mPickablesInRadius;
        public List<Pickable> PickablesOutRadius => mPickablesOutRadius;

        //==============================================================//

        public FinderNearPickables(Transform _picker, PickablesRegister _register) {
            mPicker = _picker;
            mPickableRegister = _register;
        }

        public void Find() {
            var pickables = mPickableRegister.Pickables;

            Pickable nearPickable = null;
            float nearDistance = float.MaxValue;

            mPickablesInRadius.Clear();
            mPickablesOutRadius.Clear();

            foreach (var item in pickables) {
                var sqrDistance = (mPicker.position - item.transform.position).sqrMagnitude;

                if (sqrDistance < nearDistance) {
                    nearDistance = sqrDistance;
                    nearPickable = item;
                }

                if (sqrDistance < mPickSqrRadius)
                    mPickablesInRadius.Add(item);
                else 
                    mPickablesOutRadius.Add(item);                
            }

            mNearest = nearDistance > mPickSqrRadius ? null : nearPickable;
        }
    }
}