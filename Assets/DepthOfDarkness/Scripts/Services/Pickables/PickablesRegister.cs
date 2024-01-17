using System.Collections.Generic;

namespace DD.Game {
    public class PickablesRegister {
        private List<Pickable> mPickables = new() { };
        public List<Pickable> Pickables => mPickables;

        public void AddPickable(Pickable _pickable) {
            mPickables.Add(_pickable);
        }

        public void RemovePickable(Pickable _pickable) {
            mPickables.Remove(_pickable);
        }
    }
}