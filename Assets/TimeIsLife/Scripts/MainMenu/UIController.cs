using UnityEngine;
using Zenject;

namespace TIL.MainMenu {
    public sealed class UIController : MonoBehaviour, ILifecycleListener {
        private SceneManagement sceneManagement;
        private UIView view;

        [Inject]
        public void Construct(SceneManagement _sceneManagement) {
            sceneManagement =_sceneManagement;
            view = GetComponent<UIView>();
        }

        public void OnStartGame() {
            view.OnClickPlay.AddListener(Play);
        }

        public void OnFinishGame() {
            view.OnClickPlay.RemoveListener(Play);
        }

        private void Play() {
            sceneManagement.LoadScene(SceneList.GAME);
        }
    }
}