using System.Collections.Generic;

namespace DD.Game {
    public class PickablesRegister {
        private readonly List<Pickable> m_pickables = new() { };
        public List<Pickable> Pickables => m_pickables;

        public void AddPickable(Pickable _pickable) {
            m_pickables.Add(_pickable);
        }

        public void RemovePickable(Pickable _pickable) {
            m_pickables.Remove(_pickable);
        }
    }
}