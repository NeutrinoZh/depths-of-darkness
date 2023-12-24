using UnityEngine.Tilemaps;
using UnityEngine;
using Zenject;

namespace DD.Game.ProGeneration {
    public class ProGeneration : IPreloadService {
        private Tilemap mTileMap;
        private ProGenerationParams mParams;

        [Inject]
        public ProGeneration(Tilemap _tilemap, ProGenerationParams _params) {
            mTileMap = _tilemap;
            mParams = _params;
        }

        void IPreloadService.Execute() {
            for (int i = 0; i < mParams.Size.x; ++i)
                for (int j = 0; j < mParams.Size.y; ++j)
                    mTileMap.SetTile(
                        new Vector3Int(i, j, 0), 
                        mParams.Tiles[0].Tile
                    );
        }
    }
}