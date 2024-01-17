using UnityEngine.Tilemaps;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

using Random = Unity.Mathematics.Random;

namespace DD.Game.ProGeneration {
    public class ProGeneration : MonoBehaviour {

        //=============================================//
        // Consts 

        private const uint c_random_seed = 456453;

        private readonly Vector3Int m_startPosition = new(32, 0);

        //=============================================//
        // Dependencies

        [SerializeField] private ProGenerationParams m_params;

        [SerializeField] private Tilemap m_background;
        [SerializeField] private Tilemap m_foreground;

        [SerializeField] private Transform m_oreParent;


        //=============================================//
        // Members 

        private Random m_random;

        //=============================================//
        // Lifecycles 

        void Awake() {
            m_random = Random.CreateFromIndex(c_random_seed);

            Background();
            Foreground();
            AddOre();
        }

        //=======================================================//
        // Internal 

        private void AddOre() {
            for (int i = 0; i < m_params.VioletOreCount; ++i) {
                Vector3Int randPosition = new(0, 0, 0);
                int j = 0;

                do {
                    randPosition.x = m_random.NextInt(1, m_params.Size.x - 1);
                    randPosition.y = m_random.NextInt(1, m_params.Size.y - 1);
                    j += 1;
                } while (m_foreground.GetTile(randPosition) != null && j < 1000);

                if (j >= 1000) {
                    Debug.LogError("impossible to place ore");
                    return;
                }

                Vector3 position = randPosition;
                /*mGameObservable.CreateInstance(
                    m_params.VioletOre,
                    position * 0.5f + m_params.VioletOffset,
                    Quaternion.identity,
                    m_oreParent
                );*/
            }
        }

        private void Foreground() {
            TileType[,] map = new TileType[m_params.Size.x, m_params.Size.y];
            for (int i = 0; i < m_params.Size.x; ++i)
                for (int j = 0; j < m_params.Size.y; ++j)
                    map[i, j] = TileType.WALL;

            Vector3Int min = new(1, 0);
            Vector3Int max = new(m_params.Size.x - 2, m_params.Size.y - 2);

            Vector3Int startPosition = m_startPosition;
            int n = 0, it = 0;
            const int maxIt = 1000000;

            while (n < m_params.Capacity) {
                if (map[startPosition.x, startPosition.y] == TileType.WALL)
                    n += 1;

                map[startPosition.x, startPosition.y] = TileType.AIR;

                int rand = m_random.NextInt(4);
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
                if (it > maxIt) {
                    Debug.LogError("impossible to generate dungeon");
                    break;
                }
            }

            for (int i = -m_params.BorderWidth; i < m_params.Size.x + m_params.BorderWidth; ++i)
                for (int j = 0; j < m_params.Size.y + m_params.BorderWidth; ++j)
                    m_foreground.SetTile(
                        new Vector3Int(i, j),
                        m_params.GetTile(
                            i >= 0 && j >= 0 && i < m_params.Size.x && j < m_params.Size.y ?
                                map[i, j] : TileType.WALL
                        )
                    );
        }

        private void Background() {
            for (int i = 0; i < m_params.Size.x; ++i)
                for (int j = 0; j < m_params.Size.y; ++j) {
                    Vector3Int position = new Vector3Int(i, j, 0);
                    m_background.SetTile(
                        position,
                        m_params.GetTile(BackTileOnPos(position))
                    );
                }
        }

        private TileType BackTileOnPos(Vector3Int _position) {
            Vector2 fpos = new Vector2(
                _position.x / ((float)m_params.Size.x / 5),
                _position.y / ((float)m_params.Size.y / 5)
            );

            var p = noise.cnoise(new float2(fpos.x, fpos.y));
            if (p > 0.4f)
                return TileType.DIRT;

            return TileType.STONE;
        }

        //=======================================================//
    }
}