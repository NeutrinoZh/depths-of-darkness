using UnityEngine;
using UnityEngine.UIElements;

using System;

namespace DD.MainMenu {
    public class MultiplayerMenuView : MonoBehaviour, IPage {

        //=============================================//
        // Events

        public Action OnClickHost = null;
        public Action OnClickJoin = null;
        public Action OnClickBack = null;

        //=============================================//
        // Consts

        const string c_hostButtonName = "Host";
        const string c_joinButtonName = "Join";
        const string c_backButtonName = "Back";

        //=============================================//
        // Members

        private UIDocument m_document;

        private Button m_btnHost;
        private Button m_btnJoin;
        private Button m_btnBack;

        //=============================================//
        // Lifecycle

        void Awake() {
            m_document = GetComponent<UIDocument>();

            m_btnHost = m_document.rootVisualElement.Query<Button>(c_hostButtonName);
            m_btnJoin = m_document.rootVisualElement.Query<Button>(c_joinButtonName);
            m_btnBack = m_document.rootVisualElement.Query<Button>(c_backButtonName);

            m_btnHost.clicked += OnHostHandle;
            m_btnJoin.clicked += OnJoinHandle;
            m_btnBack.clicked += OnBackHandle;

            (this as IPage).Unactivate();
        }

        void OnDestroy() {
            m_btnHost.clicked -= OnHostHandle;
            m_btnJoin.clicked -= OnJoinHandle;
            m_btnBack.clicked -= OnBackHandle;
        }

        //===========================================// 
        // Handlers

        private void OnHostHandle() {
            OnClickHost?.Invoke();
        }

        private void OnJoinHandle() {
            OnClickJoin?.Invoke();
        }

        private void OnBackHandle() {
            OnClickBack?.Invoke();
        }

        //====================================//
        // IPage

        void IPage.Activate() {
            m_document.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        void IPage.Unactivate() {
            m_document.rootVisualElement.style.display = DisplayStyle.None;
        }

        //=================================================//
    }
}