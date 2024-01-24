using System;

using UnityEngine;

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

        private void Awake() {
            m_lampState = GetComponent<LampState>();
            m_lightAnimation = GetComponent<LightAnimation>();

            m_lampState.OnOilLevelChange += OilLevelChangeHandle;
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
    }
}

