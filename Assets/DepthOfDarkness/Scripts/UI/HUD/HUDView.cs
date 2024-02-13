using UnityEngine;
using UnityEngine.UIElements;

using Zenject;

namespace DD.Game {
    public class HUDView : MonoBehaviour {
        //===============================//
        // Dependencies 

        private PlayerState m_playerState;
        private PlayerProxy m_playerProxy;

        //===============================//
        // Members 

        private UIDocument m_document;
        private Label m_oreCount;

        //===============================//
        // Lifecycle

        [Inject]
        public void Construct(PlayerProxy _playerProxy) {
            m_playerProxy = _playerProxy;
            m_playerProxy.OnSelfConnected += PlayerConnectHandle;
        }

        private void Awake() {
            m_document = GetComponent<UIDocument>();
            m_oreCount = m_document.rootVisualElement.Query<Label>();
        }

        private void OnDestroy() {
            if (m_playerState)
                m_playerState.OnChangeOreCount -= UpdateOreCountHandle;

            m_playerProxy.OnPlayerConnected -= PlayerConnectHandle;
        }

        //===============================//
        // Handlers

        private void PlayerConnectHandle(Transform _playerTransform) {
            m_playerState = _playerTransform.GetComponent<PlayerState>();
            m_playerState.OnChangeOreCount += UpdateOreCountHandle;
            UpdateOreCountHandle();
        }

        private void UpdateOreCountHandle() {
            m_oreCount.text = m_playerState.OreCount.ToString();
        }

        //===============================//
    }
}