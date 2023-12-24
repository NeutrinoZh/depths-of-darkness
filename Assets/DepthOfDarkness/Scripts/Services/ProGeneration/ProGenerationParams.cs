using UnityEngine;
using System.Collections.Generic;

namespace DD.Game.ProGeneration {
    [CreateAssetMenu(menuName = "ProGeneration/ProGenerationParams")]
    public class ProGenerationParams : ScriptableObject {
        [field: SerializeField] public Vector2Int Size { get; private set; }
        [field: SerializeField] public List<DTile> Tiles { get; private set; }
    }
}