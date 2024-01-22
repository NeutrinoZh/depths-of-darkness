using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

namespace DD.Game {
    [RequireComponent(typeof(Light2D))]
    public class LightAnimation : MonoBehaviour {
        [SerializeField] private Ease m_ease;

        [SerializeField] private float m_minIntensity;
        [SerializeField] private float m_maxIntensity;
        [SerializeField] private float m_intensityAnimDuration;

        [SerializeField] private float m_minOuterRadius;
        [SerializeField] private float m_maxOuterRadius;
        [SerializeField] private float m_outerRadiusAnimDuration;

        private Light2D m_light = null;

        public void ChangeAnimationParameters(float _minIntensity, float _maxIntensity, float _intensityAnimDuration, float _minOuterRadius, float _maxOuterRadius, float _outerRadiusAnimDuration) {
            m_minIntensity = _minIntensity;
            m_maxIntensity = _maxIntensity;
            m_intensityAnimDuration = _intensityAnimDuration;

            m_minOuterRadius = _minOuterRadius;
            m_maxOuterRadius = _maxOuterRadius;
            m_outerRadiusAnimDuration = _outerRadiusAnimDuration;
        }

        private void Awake() {
            m_light = GetComponent<Light2D>();
            Assert.AreNotEqual(m_light, null);

            float delay = Random.Range(0f, 1f);

            IntensityAnimation(delay);
            OuterRadiusAnimation(delay);
        }

        private void IntensityAnimation(float _delay) {
            m_light.intensity = m_maxIntensity;

            DOTween
                .To(
                    () => m_light.intensity,
                    x => m_light.intensity = x,
                    m_minIntensity,
                    m_intensityAnimDuration
                )
                .SetEase(m_ease)
                .SetLoops(2, LoopType.Yoyo)
                .SetDelay(_delay)
                .OnComplete(() => IntensityAnimation(0));
        }

        private void OuterRadiusAnimation(float _delay) {
            m_light.pointLightOuterRadius = m_maxOuterRadius;

            DOTween
                .To(
                    () => m_light.pointLightOuterRadius,
                    x => m_light.pointLightOuterRadius = x,
                    m_minOuterRadius,
                    m_outerRadiusAnimDuration
                )
                .SetEase(m_ease)
                .SetLoops(2, LoopType.Yoyo)
                .SetDelay(_delay)
                .OnComplete(() => OuterRadiusAnimation(0));
        }
    }
}
