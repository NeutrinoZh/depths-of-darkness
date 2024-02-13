using DD.MainMenu;

using UnityEngine;
using UnityEngine.UIElements;

namespace DD.Game {
    public class PauseMenuView : MonoBehaviour, IPage {
        [SerializeField] private HUDController m_hudController;
        [SerializeField] private UIDocument m_document;
        private bool m_isEneble = false;

        private void Awake() {
            m_hudController.OnPauseButtonClick += PauseClickHandle;

            (this as IPage).Unactivate();
        }

        private void PauseClickHandle() {
            m_isEneble = !m_isEneble;

            if (m_isEneble) {
                (this as IPage).Activate();
                Time.timeScale = 0f;
            } else {
                (this as IPage).Unactivate();
                Time.timeScale = 1f;
            }
        }

        void IPage.Activate() {
            m_document.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        void IPage.Unactivate() {
            m_document.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}
