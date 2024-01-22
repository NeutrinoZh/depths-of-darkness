using System;

using UnityEngine;

namespace DD.Game {
    [Serializable]
    public struct LampConfigaration {
        public float HalfFullMinIntensity;
        public float HalfFullIntensityAnimDuration;

        public float HalfFullMinOuterRadius;
        public float HalfFullOuterRadiusAnimDuration;

        public float LessHalfFullMinIntensity;
        public float LessHalfFullIntensityAnimDuration;

        public float LessHalfFullMinOuterRadius;
        public float LessHalfFullOuterRadiusAnimDuration;
    }

    [RequireComponent(typeof(LampState))]
    [RequireComponent(typeof(LightAnimation))]
    public class LampController : MonoBehaviour {
        [SerializeField] private LampConfigaration m_config;

        private LampState m_lampState = null;
        private LightAnimation m_lampAnimation = null;


        private void Awake() {
            m_lampState = GetComponent<LampState>();
            m_lampAnimation = GetComponent<LightAnimation>();
            m_lampState.OnOilLevelChange += OilLevelChangeHandle;
        }

        private void Update() {
            m_lampState.CheckCurrentOil();
        }

        private void OilLevelChangeHandle(LampState.EOilLevel _oilLevel) {
            switch (_oilLevel) {

                case LampState.EOilLevel.HalfFull:

                    m_lampAnimation.ChangeIntensityParameters(
                        m_config.HalfFullMinIntensity,
                        m_config.HalfFullIntensityAnimDuration
                    );

                    m_lampAnimation.ChangeOuterRadiusParameters(
                        m_config.HalfFullMinOuterRadius,
                        m_config.HalfFullOuterRadiusAnimDuration
                    );

                    break;

                case LampState.EOilLevel.QuarterFull:

                    m_lampAnimation.ChangeIntensityParameters(
                        m_config.LessHalfFullMinIntensity,
                        m_config.LessHalfFullIntensityAnimDuration
                    );

                    m_lampAnimation.ChangeOuterRadiusParameters(
                        m_config.LessHalfFullMinOuterRadius,
                        m_config.LessHalfFullOuterRadiusAnimDuration
                    );

                    break;

                default:
                    break;
            }
        }
    }
}

