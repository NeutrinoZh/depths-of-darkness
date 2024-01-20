using UnityEngine;

using DG.Tweening;

namespace DD.MainMenu {
    // It's here because TrolleyAnimation actually run animation on main menu page (ui view)
    public sealed class TrolleyAnimation : MonoBehaviour {
        [SerializeField] private Transform m_world;
        [SerializeField] private float m_cartAnimationDuration = 3;
        [SerializeField] private MainMenuView m_mainPage = null;

        public void Play() {
            m_mainPage.StartUIAnimation();
            m_world.DOMoveX(-10, m_cartAnimationDuration);
        }
    }
}