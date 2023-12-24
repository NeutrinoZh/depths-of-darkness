using Zenject;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DD.Game.ProGeneration {
    public sealed class ProGenerationServiceRegistrator : MonoBehaviour {
        [SerializeField] private ProGenerationParams mParams;
        [SerializeField] private Tilemap mTileMap;

        private SceneManagement mSceneManagement;

        [Inject]
        public void Construct(SceneManagement _sceneManagement) {
            mSceneManagement =_sceneManagement;
        }

        public void Awake() {
            mSceneManagement.AddPreloadService(new ProGeneration(mTileMap, mParams));
        }
    }
}