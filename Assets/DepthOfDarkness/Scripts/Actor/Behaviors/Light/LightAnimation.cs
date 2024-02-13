using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

namespace DD.Game {
    [RequireComponent(typeof(Light2D))]
    public class LightAnimation : MonoBehaviour {
        public event Action OnAnimationComplete;
        [SerializeField] private Ease m_ease;

        [SerializeField] private float m_minIntensity;
        [SerializeField] private float m_maxIntensity;
        [SerializeField] private float m_intensityAnimDuration;

        [SerializeField] private float m_minOuterRadius;
        [SerializeField] private float m_maxOuterRadius;
        [SerializeField] private float m_outerRadiusAnimDuration;

        private Light2D m_light = null;

        private bool m_isIntensityStepEven = false;
        private bool m_isRadiusStepEven = false;

        public void ChangeAnimationParameters(float _minIntensity, float _maxIntensity, float _intensityAnimDuration, float _minOuterRadius, float _maxOuterRadius, float _outerRadiusAnimDuration) {
            m_minIntensity = _minIntensity;
            m_maxIntensity = _maxIntensity;
            m_intensityAnimDuration = _intensityAnimDuration;

            m_minOuterRadius = _minOuterRadius;
            m_maxOuterRadius = _maxOuterRadius;
            m_outerRadiusAnimDuration = _outerRadiusAnimDuration;
        }

        public void ChangeAnimationDurations(float _intensityAnimDuration, float _outerRadiusAnimDuration) {
            m_intensityAnimDuration = _intensityAnimDuration;
            m_outerRadiusAnimDuration = _outerRadiusAnimDuration;
        }

        private void Awake() {
            m_light = GetComponent<Light2D>();
            Assert.AreNotEqual(m_light, null);

            float delay = Random.Range(0f, 1f);

            IntensityAnimation(delay, m_minIntensity);
            OuterRadiusAnimation(delay, m_minOuterRadius);
        }

        private void IntensityAnimation(float _delay, float _intensity) {
            DOTween
                .To(
                    () => m_light.intensity,
                    x => m_light.intensity = x,
                    _intensity,
                    m_intensityAnimDuration
                )
                .SetEase(m_ease)
                .SetDelay(_delay)
                .OnComplete(() => {
                    OnAnimationComplete?.Invoke();
                    m_isIntensityStepEven = !m_isIntensityStepEven;
                    IntensityAnimation(0, m_isIntensityStepEven ? m_minIntensity : m_maxIntensity);
                });
        }

        private void OuterRadiusAnimation(float _delay, float _outerRadius) {
            DOTween
                .To(
                    () => m_light.pointLightOuterRadius,
                    x => m_light.pointLightOuterRadius = x,
                    _outerRadius,
                    m_outerRadiusAnimDuration
                )
                .SetEase(m_ease)
                .SetDelay(_delay)
                .OnComplete(() => {
                    m_isRadiusStepEven = !m_isRadiusStepEven;
                    OuterRadiusAnimation(0, m_isRadiusStepEven ? m_minOuterRadius : m_maxOuterRadius);
                });
        }
    }
}
