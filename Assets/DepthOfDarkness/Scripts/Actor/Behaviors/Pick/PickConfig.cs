using UnityEngine;

namespace DD.Game {
    [CreateAssetMenu(menuName = "DD/PickConfig")]
    public class PickConfig : ScriptableObject {
        [field: SerializeField] public float PickRadius { get; private set; }
        [field: SerializeField] public Shader Highlight { get; private set; }
    }
}