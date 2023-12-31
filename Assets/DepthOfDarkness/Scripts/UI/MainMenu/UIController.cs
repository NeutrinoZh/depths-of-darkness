using DG.Tweening;
using UnityEngine;
using Zenject;

namespace DD.MainMenu {
    public sealed class UIController : MonoBehaviour, ILifecycleListener {
        [SerializeField] private Transform mWorld;
        [SerializeField] private float mCartAnimationDuration = 3;

        private SceneManagement mSceneManagement;
        private UIView mView;

        [Inject]
        public void Construct(SceneManagement _sceneManagement) {
            mSceneManagement =_sceneManagement;
            mView = GetComponent<UIView>();
        }

        void ILifecycleListener.OnStart() {
            mView.OnClickCartLabel += StartCartAnimation;
            mView.OnClickPlay += Play;
        }

        void ILifecycleListener.OnFinish() {
            mView.OnClickCartLabel -= StartCartAnimation;
            mView.OnClickPlay -= Play;
        }

        private void StartCartAnimation() {
            mView.StartUIAnimation();
            mWorld.DOMoveX(-10, mCartAnimationDuration);
        }

        private void Play() {
            mSceneManagement.LoadScene(SceneList.GAME);
        }
    }
}