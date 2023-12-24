using UnityEngine;
using UnityEngine.Tilemaps;

namespace DD.Game.ProGeneration {
    [CreateAssetMenu(menuName = "ProGeneration/Tile")]
    public class DTile : ScriptableObject {
        [field: SerializeField] public TileType Type { get; private set; }
        [field: SerializeField] public TileBase Tile { get; private set; }
    }
}