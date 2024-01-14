using UnityEngine;

namespace DD.Game {
    /// <summary>
    /// This we store references on all parents(pools) of objects.
    /// You can get it by Zenject
    /// </summary>
    public class WorldManager : MonoBehaviour {
        [field: SerializeField] public Transform Players { get; private set; }
        [field: SerializeField] public Transform Items { get; private set; }
    }
}