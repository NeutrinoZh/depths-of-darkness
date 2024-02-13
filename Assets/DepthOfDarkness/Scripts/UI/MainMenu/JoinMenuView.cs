using UnityEngine;
using UnityEngine.UIElements;

using System;

namespace DD.MainMenu {
    public class JoinMenuView : MonoBehaviour, IPage {

        //=====================================================//

        public event Action OnClickJoin = null;
        public event Action OnClickBack = null;

        public string RoomCode {
            get => m_roomCodeField.text;
        }


        //=================================================//

        const string c_roomCodeFieldName = "RoomCode";
        const string c_joinButtonName = "Join";
        const string c_backButtonName = "Back";

        //=====================================================//

        private UIDocument m_document;

        private TextInputBaseField<string> m_roomCodeField;
        private Button m_btnJoin;
        private Button m_btnBack;

        //=====================================================//

        void Awake() {
            m_document = GetComponent<UIDocument>();

            m_roomCodeField = m_document.rootVisualElement.Query<TextInputBaseField<string>>(c_roomCodeFieldName);
            m_btnJoin = m_document.rootVisualElement.Query<Button>(c_joinButtonName);
            m_btnBack = m_document.rootVisualElement.Query<Button>(c_backButtonName);

            m_btnJoin.clicked += OnJoinHandle;
            m_btnBack.clicked += OnBackHandle;

            (this as IPage).Unactivate();
        }

        void OnDestroy() {
            m_btnJoin.clicked -= OnJoinHandle;
            m_btnBack.clicked -= OnBackHandle;
        }

        //====================================================//

        private void OnJoinHandle() {
            OnClickJoin?.Invoke();
        }

        private void OnBackHandle() {
            OnClickBack?.Invoke();
        }

        //=====================================================//
        // IPage

        void IPage.Activate() {
            m_document.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        void IPage.Unactivate() {
            m_document.rootVisualElement.style.display = DisplayStyle.None;
        }

        //=====================================================//

    }
}