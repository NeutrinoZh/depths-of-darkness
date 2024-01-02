using DG.Tweening;
using UnityEngine;
using Zenject;

namespace DD.MainMenu {
    public sealed class UIController : MonoBehaviour, ILifecycleListener {
        [SerializeField] private Transform mWorld;
        [SerializeField] private float mCartAnimationDuration = 3;

        private SceneManagement mSceneManagement;

        [SerializeField] private MainMenuView mMainPage;
        [SerializeField] private MultiplayerMenuView mMultiplayerPage;
        [SerializeField] private JoinMenuView mJoinPage;

        private IPage mActivePage = null;

        [Inject]
        public void Construct(SceneManagement _sceneManagement) {
            mSceneManagement =_sceneManagement;
        }

        //======================================================//

        void ILifecycleListener.OnStart() {
            ActivatePage(mMainPage);

            // Main menu
            mMainPage.OnClickCartLabel += StartCartAnimation;
            mMainPage.OnClickPlay += PlaySolo;
            mMainPage.OnClickOnline += ToMultipalyerPage;

            // Multipalyer menu
            mMultiplayerPage.OnClickHost += Host;
            mMultiplayerPage.OnClickBack += ToMainMenuPage;
            mMultiplayerPage.OnClickJoin += ToJoinPage;

            // Join menu
            mJoinPage.OnClickJoin += Join;
            mJoinPage.OnClickBack += ToMultipalyerPage;
        }

        void ILifecycleListener.OnFinish() {

            // Main menu
            mMainPage.OnClickCartLabel -= StartCartAnimation;
            mMainPage.OnClickPlay -= PlaySolo;
            mMainPage.OnClickOnline -= ToMultipalyerPage;
        
            // Multipalyer menu
            mMultiplayerPage.OnClickBack -= ToMainMenuPage;
            mMultiplayerPage.OnClickJoin -= ToJoinPage;
            mMultiplayerPage.OnClickHost -= Host;

            // Join menu
            mJoinPage.OnClickBack -= ToMultipalyerPage;
            mJoinPage.OnClickJoin -= Join;
        }

        //======================================================//
        // Navigation

        // activate one page
        private void ActivatePage(IPage _page) {
            if (mActivePage != null)
                mActivePage.Unactivate();

            mActivePage = _page;
            mActivePage.Activate();
        }

        //

        private void ToMultipalyerPage() {
            ActivatePage(mMultiplayerPage);            
        }

        private void ToJoinPage() {
            ActivatePage(mJoinPage);
        }

        private void ToMainMenuPage() {
            ActivatePage(mMainPage);
        }

        
        //======================================================//

        private void StartCartAnimation() {
            mMainPage.StartUIAnimation();
            mWorld.DOMoveX(-10, mCartAnimationDuration);
        }

        private void PlaySolo() {
            mSceneManagement.LoadScene(SceneList.GAME);
        }

        private void Host() {
            mSceneManagement.LoadScene(SceneList.GAME);
        }

        private void Join() {
            mSceneManagement.Join(mJoinPage.RoomCode);
        }
    }
}