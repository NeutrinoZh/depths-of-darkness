using UnityEngine;
using Zenject;

namespace TIL.MainMenu {
    public sealed class UIController : MonoBehaviour, ILifecycleListener {
        private SceneManagement sceneManagement;
        private UIView uiView;

        [Inject]
        public void Construct(SceneManagement _sceneManagement) {
            sceneManagement =_sceneManagement;
            uiView = GetComponent<UIView>();
        }

        public void OnStartGame() {
            uiView.OnClickPlay.AddListener(Play);
        }

        public void OnFinishGame() {
            uiView.OnClickPlay.RemoveListener(Play);
        }

        private void Play() {
            sceneManagement.LoadScene(SceneList.GAME);
        }
    }
}