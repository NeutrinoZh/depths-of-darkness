using UnityEngine;

namespace DD.Game {
    /// <summary>
    /// Here we store references on all parents(groups) of objects.
    /// You can get it by Zenject
    /// </summary>
    public class GroupManager : MonoBehaviour {

        [field: SerializeField] public Transform Players { get; private set; }
        [field: SerializeField] public Transform Items { get; private set; }
        [field: SerializeField] public Transform Ores { get; private set; }

    }
}