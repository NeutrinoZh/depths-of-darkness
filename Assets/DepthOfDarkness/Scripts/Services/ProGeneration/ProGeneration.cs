using UnityEngine.Tilemaps;
using Unity.Mathematics;
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
                for (int j = 0; j < mParams.Size.y; ++j) {
                    Vector3Int positon = new Vector3Int(i, j, 0);

                    if (mTileMap.GetTile(positon) != null)
                        continue;

                    mTileMap.SetTile(
                        positon,    
                        mParams.GetTile(
                            TileOnPos(positon)
                        )
                    );
                }
        }

        private TileType TileOnPos(Vector3Int _position) {
            Vector2 fpos = new Vector2(
                _position.x / ((float)mParams.Size.x / 5),
                _position.y / ((float)mParams.Size.y / 5)
            );

            var p = noise.cnoise(new float2(fpos.x, fpos.y));
            if (p > 0.4f)  
                return TileType.DIRT;

            return TileType.STONE;
        }
    }
}