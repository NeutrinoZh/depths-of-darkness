using Zenject;
using UnityEngine;

namespace DD.Game.ProGeneration {
    public sealed class ProGenerationServiceRegistrator : MonoBehaviour {
        [SerializeField] private ProGeneration mInstance;
        private SceneManagement mSceneManagement;

        [Inject]
        public void Construct(SceneManagement _sceneManagement) {
            mSceneManagement =_sceneManagement;
        }

        public void Awake() {
            mSceneManagement.AddPreloadService(mInstance);
        }
    }
}