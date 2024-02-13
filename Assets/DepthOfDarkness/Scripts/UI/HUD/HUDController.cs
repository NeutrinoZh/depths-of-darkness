using System;

using UnityEngine;
using UnityEngine.UIElements;

namespace DD.Game {
    public class HUDController : MonoBehaviour {
        public event Action OnPauseButtonClick = null;

        const string c_pauseButtonName = "PauseButton";

        private UIDocument m_document;
        private Button m_pauseButton;

        private void Awake() {
            m_document = GetComponent<UIDocument>();
            m_pauseButton = m_document.rootVisualElement.Query<Button>(c_pauseButtonName);

            m_pauseButton.clicked += OnClickPauseHandle;
        }

        private void OnClickPauseHandle() {
            OnPauseButtonClick?.Invoke();
        }

        private void OnDestroy() {
            m_pauseButton.clicked -= OnClickPauseHandle;
        }
    }
}