using System.Collections.Generic;

using UnityEngine;

namespace DD.Game {
    public sealed class FinderNearPickables {
        private readonly PickablesRegister m_pickableRegister = null;
        private readonly Transform m_picker = null;

        //==============================================================//

        private const float c_pickSqrRadius = 0.7f;

        private readonly List<Pickable> m_pickablesInRadius = new();
        private readonly List<Pickable> m_pickablesOutRadius = new();

        private Pickable m_Nearest = null;

        //==============================================================//

        public Pickable Nearest => m_Nearest;
        public List<Pickable> PickablesInRadius => m_pickablesInRadius;
        public List<Pickable> PickablesOutRadius => m_pickablesOutRadius;

        //==============================================================//

        public FinderNearPickables(Transform _picker, PickablesRegister _register) {
            m_picker = _picker;
            m_pickableRegister = _register;
        }

        public void Find() {
            var pickables = m_pickableRegister.Pickables;

            Pickable nearPickable = null;
            float nearDistance = float.MaxValue;

            m_pickablesInRadius.Clear();
            m_pickablesOutRadius.Clear();

            foreach (var item in pickables) {
                var sqrDistance = (m_picker.position - item.transform.position).sqrMagnitude;

                if (sqrDistance < nearDistance) {
                    nearDistance = sqrDistance;
                    nearPickable = item;
                }

                if (sqrDistance < c_pickSqrRadius)
                    m_pickablesInRadius.Add(item);
                else
                    m_pickablesOutRadius.Add(item);
            }

            m_Nearest = nearDistance > c_pickSqrRadius ? null : nearPickable;
        }
    }
}