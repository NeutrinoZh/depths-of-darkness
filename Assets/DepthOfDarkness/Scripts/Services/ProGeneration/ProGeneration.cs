using UnityEngine.Tilemaps;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace DD.Game.ProGeneration {
    public class ProGeneration : MonoBehaviour, IPreloadService {
        private readonly Vector3Int mStartPosition = new(32, 0);

        // Params 
        [SerializeField] private ProGenerationParams mParams;
        
        // Tilemaps
        [SerializeField] private Tilemap mBackground;
        [SerializeField] private Tilemap mForeground;

        // 
        [SerializeField] private Transform mOreParent;

        // dependencies
        private GameObservable mGameObservable;

        [Inject]
        public void Consturct(GameObservable _gameObservable) {
            mGameObservable = _gameObservable;
        }

        void IPreloadService.Execute() {
            Background();
            Foreground();
            AddOre();
        }

        //=======================================================//
        // Ore

        private void AddOre() {
            for (int i = 0; i < mParams.VioletOreCount; ++i) {
                Vector3Int randPosition = new(0, 0, 0);
                int j = 0;

                do {
                    randPosition.x = UnityEngine.Random.Range(1, mParams.Size.x - 1);
                    randPosition.y = UnityEngine.Random.Range(1, mParams.Size.y - 1);
                    j += 1;
                } while (mForeground.GetTile(randPosition) != null && j < 1000);
                
                if (j >= 1000) {
                    Debug.LogError("impossible to place ore");
                    return;
                }

                Vector3 position = randPosition;
                mGameObservable.CreateInstance(
                    mParams.VioletOre,
                    position * 0.5f + mParams.VioletOffset,
                    Quaternion.identity,
                    mOreParent
                );
            }
        }

        //=======================================================//
        // Foreground

        private void Foreground() {
            TileType[,] map = new TileType[mParams.Size.x, mParams.Size.y];
            for (int i = 0; i < mParams.Size.x; ++i)
                for (int j = 0; j < mParams.Size.y; ++j)
                    map[i, j] = TileType.WALL;

            Vector3Int min = new(1, 0);
            Vector3Int max = new(mParams.Size.x - 2, mParams.Size.y - 2);

            Vector3Int startPosition = mStartPosition;
            int n = 0, it = 0;
            while (n < mParams.Capacity) {
                if (map[startPosition.x, startPosition.y] == TileType.WALL)
                    n += 1;

                map[startPosition.x, startPosition.y] = TileType.AIR;

                int rand = UnityEngine.Random.Range(0, 4);
                switch (rand) {
                    case 0:
                        startPosition.x += 1;
                        break;
                    case 1:
                        startPosition.x -= 1;
                        break;
                    case 2:
                        startPosition.y += 1;
                        break;
                    case 3:
                        startPosition.y -= 1;
                        break;
                    default:
                        break;
                }

                startPosition.Clamp(min, max);

                ++it;
                if (it > 1000) {
                    Debug.LogError("impossible to generate dungeon");
                    break;
                }
            }   

            for (int i = -mParams.BorderWidth; i < mParams.Size.x + mParams.BorderWidth; ++i)
                for (int j = 0; j < mParams.Size.y + mParams.BorderWidth; ++j)
                    mForeground.SetTile(
                        new Vector3Int(i, j),
                        mParams.GetTile(
                            i >= 0 && j >= 0 && i < mParams.Size.x && j < mParams.Size.y ? 
                                map[i, j] : TileType.WALL
                        )
                    );
        }

        //=======================================================//
        // Background 

        private void Background() {
             for (int i = 0; i < mParams.Size.x; ++i)
                for (int j = 0; j < mParams.Size.y; ++j) {
                    Vector3Int position = new Vector3Int(i, j, 0);
                    mBackground.SetTile(
                        position,    
                        mParams.GetTile(BackTileOnPos(position))
                    );
            }
        }

        private TileType BackTileOnPos(Vector3Int _position) {
            Vector2 fpos = new Vector2(
                _position.x / ((float)mParams.Size.x / 5),
                _position.y / ((float)mParams.Size.y / 5)
            );

            var p = noise.cnoise(new float2(fpos.x, fpos.y));
            if (p > 0.4f)  
                return TileType.DIRT;

            return TileType.STONE;
        }

        //=======================================================//
    }
}