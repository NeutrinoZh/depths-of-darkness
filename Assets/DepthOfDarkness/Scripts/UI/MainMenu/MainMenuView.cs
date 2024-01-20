using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System;

namespace DD.MainMenu {
    public sealed class MainMenuView : MonoBehaviour, IPage {
        //===================================//
        // Events
        public Action OnClickPlay;
        public Action OnClickOnline;
        public Action OnClickCartLabel;

        //===================================//
        // Consts

        const string c_playButtonName = "PlaySolo";
        const string c_playOnlineButtonName = "PlayOnline";
        const string c_cartLabelName = "CartLabel";
        const string c_btnScreenName = "BtnScreen";

        const string c_rootDivName = "RootDiv";
        const string c_rootDivAnimateClass = "animate";

        //===================================//
        // Members 

        private UIDocument m_document;

        private VisualElement m_rootDiv;
        private VisualElement m_btnScreen;

        private VisualElement m_cartLabel;
        private Button m_btnPlay;
        private Button m_btnMultiplayer;

        //===================================//
        // Lifecycle

        private void Awake() {
            // gettings 
            m_document = GetComponent<UIDocument>();
            m_btnPlay = m_document.rootVisualElement.Query<Button>(c_playButtonName);
            m_btnMultiplayer = m_document.rootVisualElement.Query<Button>(c_playOnlineButtonName);
            m_btnScreen = m_document.rootVisualElement.Query(c_btnScreenName);

            m_rootDiv = m_document.rootVisualElement.Query(c_rootDivName);
            m_cartLabel = m_document.rootVisualElement.Query(c_cartLabelName);

            // subscribes
            m_btnPlay.clicked += OnClickPlayHandle;
            m_btnMultiplayer.clicked += OnClickPlayOnlineHandle;
            m_btnScreen.RegisterCallback<ClickEvent>(OnClickCartLabelHandle);
        }

        private void Start() {
            DOTween.To(
                () => m_cartLabel.style.opacity.value,
                x => m_cartLabel.style.opacity = x,
                1, 1
            ).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy() {
            m_btnPlay.clicked -= OnClickPlayHandle;
            m_btnMultiplayer.clicked -= OnClickPlayOnlineHandle;
            m_btnScreen.UnregisterCallback<ClickEvent>(OnClickCartLabelHandle);
        }

        //===================================//
        // Handlers

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
            m_rootDiv.AddToClassList(c_rootDivAnimateClass);
        }

        //===================================//
        // IPage

        void IPage.Activate() {
            m_document.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        void IPage.Unactivate() {
            m_document.rootVisualElement.style.display = DisplayStyle.None;
        }

        //===================================//

    }
}