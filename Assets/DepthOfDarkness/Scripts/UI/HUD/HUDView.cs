using Unity.Netcode;

using UnityEngine;
using UnityEngine.UIElements;

namespace DD.Game {
    public class HUDView : MonoBehaviour {
        //===============================//
        // Dependencies 

        private PlayerState m_playerState;

        //===============================//
        // Members 

        private UIDocument m_document;
        private Label m_oreCount;

        //===============================//
        // Lifecycle

        private void Awake() {
            // FIXME: Don't use NetworkManager for it. PlayerState is dependecies
            m_playerState = NetworkManager.Singleton
                .LocalClient.PlayerObject.GetComponent<PlayerState>();

            m_document = GetComponent<UIDocument>();
            m_oreCount = m_document.rootVisualElement.Query<Label>();

            m_playerState.OnChangeOreCount += UpdateOreCountHandle;
            UpdateOreCountHandle();
        }

        private void OnDestroy() {
            m_playerState.OnChangeOreCount -= UpdateOreCountHandle;
        }

        //===============================//
        // Handlers

        private void UpdateOreCountHandle() {
            m_oreCount.text = m_playerState.OreCount.ToString();
        }

        //===============================//
    }
}