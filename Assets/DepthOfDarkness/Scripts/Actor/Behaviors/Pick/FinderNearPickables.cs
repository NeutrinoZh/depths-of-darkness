using System.Collections.Generic;

using UnityEngine;

namespace DD.Game {
    public sealed class FinderNearPickables {
        //==============================================================//
        // Props 

        public Pickable Nearest { get; private set; } = null;
        public List<Pickable> PickablesInRadius { get; } = new();
        public List<Pickable> PickablesOutRadius { get; } = new();

        //==============================================================//
        // Consts

        private const float c_pickSqrRadius = 0.7f;

        //==============================================================//
        // Members         

        private readonly PickablesRegister m_pickableRegister = null;
        private readonly Transform m_picker = null;

        //==============================================================//
        // Lifecycle 

        public FinderNearPickables(Transform _picker, PickablesRegister _register) {
            m_picker = _picker;
            m_pickableRegister = _register;
        }

        //==============================================================//
        // Public interface

        public void Find() {
            var pickables = m_pickableRegister.Pickables;

            Pickable nearPickable = null;
            float nearDistance = float.MaxValue;

            PickablesInRadius.Clear();
            PickablesOutRadius.Clear();

            foreach (var item in pickables) {
                var sqrDistance = (m_picker.position - item.transform.position).sqrMagnitude;

                if (sqrDistance < nearDistance) {
                    nearDistance = sqrDistance;
                    nearPickable = item;
                }

                if (sqrDistance < c_pickSqrRadius)
                    PickablesInRadius.Add(item);
                else
                    PickablesOutRadius.Add(item);
            }

            Nearest = nearDistance > c_pickSqrRadius ? null : nearPickable;
        }
    }
}