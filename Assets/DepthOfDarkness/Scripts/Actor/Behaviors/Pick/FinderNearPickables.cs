using System.Collections.Generic;

using UnityEngine;

namespace DD.Game {
    public sealed class FinderNearPickables {
        private readonly PickablesRegister m_pickableRegister = null;
        private readonly Transform m_picker = null;

        //==============================================================//

        private const float mPickSqrRadius = 0.7f;

        private readonly List<Pickable> mPickablesInRadius = new();
        private readonly List<Pickable> mPickablesOutRadius = new();

        private Pickable mNearest = null;

        //==============================================================//

        public Pickable Nearest => mNearest;
        public List<Pickable> PickablesInRadius => mPickablesInRadius;
        public List<Pickable> PickablesOutRadius => mPickablesOutRadius;

        //==============================================================//

        public FinderNearPickables(Transform _picker, PickablesRegister _register) {
            m_picker = _picker;
            m_pickableRegister = _register;
        }

        public void Find() {
            var pickables = m_pickableRegister.Pickables;

            Pickable nearPickable = null;
            float nearDistance = float.MaxValue;

            mPickablesInRadius.Clear();
            mPickablesOutRadius.Clear();

            foreach (var item in pickables) {
                var sqrDistance = (m_picker.position - item.transform.position).sqrMagnitude;

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