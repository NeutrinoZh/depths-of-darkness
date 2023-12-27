using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DD.Game {
    public class PickablesRegister : ILifecycleListener {
        private List<IPickable> mPickables = new() {};

        public void AddPickable(IPickable _pickable) {
            mPickables.Add(_pickable);
        }

        public void RemovePickable(IPickable _pickable) {
            mPickables.Remove(_pickable);
        }

        public List<IPickable> FindAndSortPickablesInRadius(Vector3 _point, float _sqrRadius) {
            Dictionary<IPickable, float> sqrDistances = new();

            foreach (IPickable pickable in mPickables) {
                var sqrDistance = (pickable.GetTransform().position - _point).sqrMagnitude;
                if (sqrDistance < _sqrRadius)
                    sqrDistances.Add(pickable, sqrDistance);
            }

            var sorted = (
                    from entry in sqrDistances
                    orderby entry.Value 
                    select entry.Key
                ).ToList();

            return sorted;
        }
    }
}