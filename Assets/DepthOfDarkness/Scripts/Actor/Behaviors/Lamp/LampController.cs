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
        [SerializeField] private LampConfigaration mConfig;
        private LampState mLampState = null;
        private LightAnimation mLampAnimation = null;


        private void Awake() {
            mLampState = GetComponent<LampState>();
            mLampAnimation = GetComponent<LightAnimation>();
            mLampState.OnOilLevelChange += OilLevelChangeHandle;
        }

        private void Update() {
           mLampState.CheckCurrentOil();
        }

        private void OilLevelChangeHandle(LampState.EOilLevel _oilLevel) {

            switch (_oilLevel) {
                case LampState.EOilLevel.HalfFull:
                    mLampAnimation.ChangeIntensityParameters(mConfig.HalfFullMinIntensity, mConfig.HalfFullIntensityAnimDuration);
                    mLampAnimation.ChangeOuterRadiusParameters(mConfig.HalfFullMinOuterRadius, mConfig.HalfFullOuterRadiusAnimDuration);
                    break;

                case LampState.EOilLevel.LessHalfFull:
                    mLampAnimation.ChangeIntensityParameters(mConfig.LessHalfFullMinIntensity, mConfig.LessHalfFullIntensityAnimDuration);
                    mLampAnimation.ChangeOuterRadiusParameters(mConfig.LessHalfFullMinOuterRadius, mConfig.LessHalfFullOuterRadiusAnimDuration);;
                    break;
                
                default: break;
            }
        }
    }
}

