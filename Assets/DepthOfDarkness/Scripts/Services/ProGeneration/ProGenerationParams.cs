using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace DD.Game.ProGeneration {
    [CreateAssetMenu(menuName = "ProGeneration/ProGenerationParams")]
    public class ProGenerationParams : ScriptableObject {
        [field: SerializeField] public Vector2Int Size { get; private set; }
        [field: SerializeField] public List<DTile> Tiles { get; private set; }
        
        public TileBase GetTile(TileType _type) {
            foreach (var tile in Tiles)
                if (tile.Type == _type)
                    return tile.Tile;
                
            Debug.LogError($"ProGeneration: tile {_type} is missing");
            return Tiles[0].Tile;
        }
    }
}