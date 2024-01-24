using System;

using UnityEngine;

using Random = UnityEngine.Random;

namespace DD.Game {
    [Serializable]
    public struct LampConfigaration {
        public float MinIntensity;
        public float MaxIntensity;
        public float IntensityAnimDuration;

        public float MinOuterRadius;
        public float MaxOuterRadius;
        public float OuterRadiusAnimDuration;
    }

    [RequireComponent(typeof(LampState))]
    [RequireComponent(typeof(LightAnimation))]
    public class LampController : MonoBehaviour {
        [SerializeField] private LampConfigaration[] m_config;

        private LampState m_lampState = null;
        private LightAnimation m_lightAnimation = null;

        private int m_noisesCount = 0;

        private bool m_isAnimationDurationChanged = false;

        private void Awake() {
            m_lampState = GetComponent<LampState>();
            m_lightAnimation = GetComponent<LightAnimation>();

            m_lampState.OnOilLevelChange += OilLevelChangeHandle;
            m_lightAnimation.OnAnimationComplete += AnimationChangeHandle;
        }

        private void Update() {
            m_lampState.CheckCurrentOil();
            m_lampState.UseOil();
        }

        private void OilLevelChangeHandle(LampState.EOilLevel _oilLevel) {
            m_lightAnimation.ChangeAnimationParameters(
                m_config[(int)_oilLevel].MinIntensity,
                m_config[(int)_oilLevel].MaxIntensity,
                m_config[(int)_oilLevel].IntensityAnimDuration,
                m_config[(int)_oilLevel].MinOuterRadius,
                m_config[(int)_oilLevel].MaxOuterRadius,
                m_config[(int)_oilLevel].OuterRadiusAnimDuration);
        }

        private void AnimationChangeHandle() {
            if (m_noisesCount > 1) ChangeAnimationDurations();
            else if (m_isAnimationDurationChanged) {
                m_lightAnimation.ChangeAnimationDurations(
                    m_config[(int)m_lampState.OilLevel].IntensityAnimDuration,
                    m_config[(int)m_lampState.OilLevel].OuterRadiusAnimDuration);

                m_isAnimationDurationChanged = false;
            } else StartNoises();
        }

        private void StartNoises() {
            if (m_lampState.OilLevel == LampState.EOilLevel.Full ||
                m_lampState.OilLevel == LampState.EOilLevel.Empty) return;

            float noiseChance = 0.1f * (int)m_lampState.OilLevel;

            if (Random.Range(0f, 1f) <= noiseChance) {
                ChangeAnimationDurations();

                m_noisesCount = Random.Range(2, 4) * (int)m_lampState.OilLevel;
                m_isAnimationDurationChanged = true;
            }
        }

        private void ChangeAnimationDurations() {
            m_lightAnimation.ChangeAnimationDurations(
                m_config[(int)m_lampState.OilLevel].IntensityAnimDuration * 0.2f,
                m_config[(int)m_lampState.OilLevel].OuterRadiusAnimDuration * 0.2f);

            m_noisesCount -= 1;
        }
    }
}

