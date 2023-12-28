using System.Collections.Generic;
using UnityEngine;

namespace DD.Game {
    public class Entity : MonoBehaviour {
        public List<IBehavior> Components {  get; protected set; }
        public List<IEntityState> States { get; protected set; }

        public T GetState<T>() where T : IEntityState {
            var type = typeof(T);
            foreach (IEntityState state in States)
                if (state.GetType() == type)
                    return (T)state;
            return default;
        }

        public T GetBehavior<T>() where T : IBehavior {
            var type = typeof(T);
            foreach (IBehavior state in States)
                if (state.GetType() == type)
                    return (T)state;
            return default;
        }
    }
}