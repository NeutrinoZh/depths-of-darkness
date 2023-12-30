using UnityEngine;
using Zenject;

namespace DD {
    public sealed class GameObservableRegistrator : MonoBehaviour {
        private GameObservable mGameObservable;
        private SceneManagement mSceneManagement;

        [Inject]
        public void Consturct(SceneManagement _sceneManagement, GameObservable _gameObservable) {
            mSceneManagement = _sceneManagement;
            mGameObservable = _gameObservable;
        }

        private void Awake() {
            mSceneManagement.GameObservable = mGameObservable;
        }
    }   
}