using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace DD.MainMenu {
    public sealed class UIController : MonoBehaviour, ILifecycleListener {

        // dependecies 

        private SceneManagement m_sceneManagement;
        private TrolleyAnimation m_trolleyAnimation;

        [SerializeField] private MainMenuView m_mainPage;
        [SerializeField] private MultiplayerMenuView m_multiplayerPage;
        [SerializeField] private JoinMenuView m_joinPage;

        [Inject]
        public void Construct(SceneManagement _sceneManagement) {
            m_sceneManagement = _sceneManagement;

            m_trolleyAnimation = GetComponent<TrolleyAnimation>();
            Assert.AreNotEqual(m_trolleyAnimation, null);
        }

        // internal states 

        private IPage m_ActivePage = null;

        //======================================================//

        void ILifecycleListener.OnStart() {
            ActivatePage(m_mainPage);

            // Main menu
            m_mainPage.OnClickCartLabel += m_trolleyAnimation.Play;
            m_mainPage.OnClickPlay += PlaySolo;
            m_mainPage.OnClickOnline += ToMultipalyerPage;

            // Multipalyer menu
            m_multiplayerPage.OnClickHost += Host;
            m_multiplayerPage.OnClickBack += ToMainMenuPage;
            m_multiplayerPage.OnClickJoin += ToJoinPage;

            // Join menu
            m_joinPage.OnClickJoin += Join;
            m_joinPage.OnClickBack += ToMultipalyerPage;
        }

        void ILifecycleListener.OnFinish() {

            // Main menu
            m_mainPage.OnClickCartLabel -= m_trolleyAnimation.Play;
            m_mainPage.OnClickPlay -= PlaySolo;
            m_mainPage.OnClickOnline -= ToMultipalyerPage;

            // Multipalyer menu
            m_multiplayerPage.OnClickBack -= ToMainMenuPage;
            m_multiplayerPage.OnClickJoin -= ToJoinPage;
            m_multiplayerPage.OnClickHost -= Host;

            // Join menu
            m_joinPage.OnClickBack -= ToMultipalyerPage;
            m_joinPage.OnClickJoin -= Join;
        }

        //======================================================//
        // Navigation

        // activate one page
        private void ActivatePage(IPage _page) {
            if (m_ActivePage != null)
                m_ActivePage.Unactivate();

            m_ActivePage = _page;
            m_ActivePage.Activate();
        }

        //

        private void ToMultipalyerPage() {
            ActivatePage(m_multiplayerPage);
        }

        private void ToJoinPage() {
            ActivatePage(m_joinPage);
        }

        private void ToMainMenuPage() {
            ActivatePage(m_mainPage);
        }


        //======================================================//

        private void PlaySolo() {
            m_sceneManagement.LoadScene(SceneList.GAME);
        }

        private void Host() {
            m_sceneManagement.LoadScene(SceneList.GAME);
        }

        private void Join() {
            m_sceneManagement.Join(m_joinPage.RoomCode);
        }
    }
}