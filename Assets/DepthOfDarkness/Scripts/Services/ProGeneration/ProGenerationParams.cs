using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace DD.Game.ProGeneration {
    [CreateAssetMenu(menuName = "DD/ProGeneration/ProGenerationParams")]
    public class ProGenerationParams : ScriptableObject {
        [field: SerializeField] public Vector2Int Size { get; private set; }
        [field: SerializeField] public List<DTile> Tiles { get; private set; }
        [field: SerializeField] public int Capacity { get; private set; }
        [field: SerializeField] public int BorderWidth { get; private set; }


        [field: SerializeField] public int VioletOreCount { get; private set; }
        [field: SerializeField] public Transform VioletOre { get; private set; }
        [field: SerializeField] public Vector3 VioletOffset { get; private set; }

        

        public TileBase GetTile(TileType _type) {
            if (_type == TileType.AIR)
                return null;

            foreach (var tile in Tiles)
                if (tile.Type == _type)
                    return tile.Tile;
                
            Debug.LogError($"ProGeneration: tile {_type} is missing");
            return null;
        }
    }
}