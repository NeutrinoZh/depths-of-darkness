using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    public class TrolleyAnimator : MonoBehaviour {

        //=======================================//
        // Members 

        [SerializeField] private List<Sprite> m_sprites;

        private TrolleyState m_trolleyState;
        private SpriteRenderer m_renderer;

        //=======================================//
        // Lifecycles 

        private void Awake() {
            m_trolleyState = GetComponent<TrolleyState>();
            Assert.AreNotEqual(m_trolleyState, null);

            m_renderer = GetComponent<SpriteRenderer>();
            Assert.AreNotEqual(m_renderer, null);

            m_trolleyState.OnChangeOreCount += ChangeOreCountHandle;
        }

        private void OnDestroy() {
            m_trolleyState.OnChangeOreCount -= ChangeOreCountHandle;
        }

        //=======================================//
        // Handles 

        private void ChangeOreCountHandle() {
            // progress in percent (0-1)
            float progress = m_trolleyState.OreCount / 10f;

            int chunk = (int)(progress * m_sprites.Count);
            if (chunk >= m_sprites.Count)
                chunk = m_sprites.Count - 1;

            m_renderer.sprite = m_sprites[chunk];
        }
    }
}