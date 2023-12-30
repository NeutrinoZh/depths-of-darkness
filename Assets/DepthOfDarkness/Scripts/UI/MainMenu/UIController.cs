using UnityEngine;
using Zenject;

namespace DD.MainMenu {
    public sealed class UIController : MonoBehaviour, ILifecycleListener {
        private SceneManagement sceneManagement;
        private UIView view;

        [Inject]
        public void Construct(SceneManagement _sceneManagement) {
            sceneManagement =_sceneManagement;
            view = GetComponent<UIView>();
        }

        void ILifecycleListener.OnStart() {
            view.OnClickPlay += Play;
        }

        void ILifecycleListener.OnFinish() {
            view.OnClickPlay -= Play;
        }

        private void Play() {
            sceneManagement.LoadScene(SceneList.GAME);
        }
    }
}