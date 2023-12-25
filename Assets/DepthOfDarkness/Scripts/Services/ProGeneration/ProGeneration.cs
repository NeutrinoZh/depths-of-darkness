using UnityEngine.Tilemaps;
using Unity.Mathematics;
using UnityEngine;

using Zenject;

namespace DD.Game.ProGeneration {
    public class ProGeneration : IPreloadService {
        private Tilemap mTileMap;
        private ProGenerationParams mParams;
        private int mSeed = 0;

        [Inject]
        public ProGeneration(Tilemap _tilemap, ProGenerationParams _params) {
            mTileMap = _tilemap;
            mParams = _params;
            mSeed = UnityEngine.Random.Range(-int.MaxValue/2, int.MaxValue/2);
        }

        void IPreloadService.Execute() {
            int halfW = mParams.Size.x / 2;
            int halfH = mParams.Size.y / 2;
            
            for (int i = -halfW; i < halfW; ++i)
                for (int j = -halfH; j < halfH; ++j) {
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
                _position.x / ((float)mParams.Size.x),
                _position.y / ((float)mParams.Size.y)
            );

            var p = noise.cnoise(new float2(fpos.x, fpos.y));
            if (p > 0.2f)
                return TileType.STONE;

            return ValleyBiom(_position);
        }

        private TileType ValleyBiom(Vector3Int _position) {
            Vector2 fpos = new Vector2(
                _position.x / ((float)mParams.Size.x / 10),
                _position.y / ((float)mParams.Size.y / 10)
            );

            var p = noise.cnoise(new float2(fpos.x, fpos.y));
            if (p > 0.5f)  
                return TileType.DIRT;

            return TileType.GRASS;
        }
    }
}