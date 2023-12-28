using System.Collections.Generic;

namespace DD.Game {
    public class PickablesRegister : ILifecycleListener {
        private List<IPickable> mPickables = new() {};
        public List<IPickable> Pickables => mPickables;

        public void AddPickable(IPickable _pickable) {
            mPickables.Add(_pickable);
        }

        public void RemovePickable(IPickable _pickable) {
            mPickables.Remove(_pickable);
        }
    }
}