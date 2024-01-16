using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System;

namespace DD.MainMenu {
    public sealed class MainMenuView : MonoBehaviour, ILifecycleListener, IPage {
        //===================================//
        // EVENTS
        public Action OnClickPlay;
        public Action OnClickOnline;
        public Action OnClickCartLabel;

        //=================================================//
        // IPage

        void IPage.Activate() {
            m_document.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        void IPage.Unactivate() {
            m_document.rootVisualElement.style.display = DisplayStyle.None;
        }

        //===================================//

        const string C_playButtonName = "PlaySolo";
        const string C_playOnlineButtonName = "PlayOnline";
        const string C_cartLabelName = "CartLabel";
        const string C_btnScreenName = "BtnScreen";

        const string C_rootDivName = "RootDiv";
        const string C_rootDivAnimateClass = "animate";

        //===================================//

        private UIDocument m_document;

        private VisualElement m_rootDiv;
        private VisualElement m_btnScreen;

        private VisualElement m_cartLabel;
        private Button m_btnPlay;
        private Button m_btnMultiplayer;

        //===================================//
        // ILifecycleListener

        void ILifecycleListener.OnInit() {
            // gettings 
            m_document = GetComponent<UIDocument>();
            m_btnPlay = m_document.rootVisualElement.Query<Button>(C_playButtonName);
            m_btnMultiplayer = m_document.rootVisualElement.Query<Button>(C_playOnlineButtonName);
            m_btnScreen = m_document.rootVisualElement.Query(C_btnScreenName);

            m_rootDiv = m_document.rootVisualElement.Query(C_rootDivName);
            m_cartLabel = m_document.rootVisualElement.Query(C_cartLabelName);

            // subscribes
            m_btnPlay.clicked += OnClickPlayHandle;
            m_btnMultiplayer.clicked += OnClickPlayOnlineHandle;
            m_btnScreen.RegisterCallback<ClickEvent>(OnClickCartLabelHandle);
        }

        void ILifecycleListener.OnStart() {
            // animations
            DOTween.To(
                () => m_cartLabel.style.opacity.value,
                x => m_cartLabel.style.opacity = x,
                1, 1
            ).SetLoops(-1, LoopType.Yoyo);
        }

        void ILifecycleListener.OnFinish() {
            m_btnPlay.clicked -= OnClickPlayHandle;
            m_btnMultiplayer.clicked -= OnClickPlayOnlineHandle;
            m_btnScreen.UnregisterCallback<ClickEvent>(OnClickCartLabelHandle);
        }

        //===================================//

        private void OnClickCartLabelHandle(ClickEvent _event) {
            m_btnScreen.pickingMode = PickingMode.Ignore;
            OnClickCartLabel?.Invoke();
        }

        private void OnClickPlayHandle() {
            OnClickPlay?.Invoke();
        }

        private void OnClickPlayOnlineHandle() {
            OnClickOnline?.Invoke();
        }

        //===================================//

        public void StartUIAnimation() {
            m_cartLabel.style.display = DisplayStyle.None;
            m_rootDiv.AddToClassList(C_rootDivAnimateClass);
        }
    }
}