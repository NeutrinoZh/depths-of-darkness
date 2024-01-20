using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

namespace DD.Game  { 
    [RequireComponent(typeof(Light2D))]
    public class LightAnimation : MonoBehaviour, ILifecycleListener {
        [SerializeField] private Ease mEase;

        [SerializeField] private float mMinIntensity;
        [SerializeField] private float mIntensityAnimDuration;

        [SerializeField] private float mMinOuterRadius;
        [SerializeField] private float mOuterRadiusAnimDuration;

        private Light2D mLight = null;

        public void ChangeIntensityParameters(float _minIntensity, float _intensityAnimDuration) {
            mMinIntensity = _minIntensity;
            mIntensityAnimDuration = _intensityAnimDuration;
        }

        public void ChangeOuterRadiusParameters(float _minOuterRadius, float _outerRadiusAnimDuration) {
            mMinOuterRadius = _minOuterRadius;
            mOuterRadiusAnimDuration = _outerRadiusAnimDuration;
        }

        private void Awake() {
            mLight = GetComponent<Light2D>();
            Assert.AreNotEqual(mLight, null);

            float delay = Random.Range(0f, 1f);

            IntensityAnimation(delay);
            OuterRadiusAnimation(delay);  
        }

        private void IntensityAnimation(float _delay) {
            DOTween
                .To(
                    () => mLight.intensity,
                    x => mLight.intensity = x,
                    mMinIntensity,
                    mIntensityAnimDuration
                )
                .SetEase(mEase)
                .SetLoops(2, LoopType.Yoyo)
                .SetDelay(_delay)
                .OnComplete(()=> {
                    IntensityAnimation(0);
                });
        }

        private void OuterRadiusAnimation(float _delay) {
            DOTween
                .To(
                    () => mLight.pointLightOuterRadius,
                    x => mLight.pointLightOuterRadius = x,
                    mMinOuterRadius, 
                    mOuterRadiusAnimDuration
                )
                .SetEase(mEase)
                .SetLoops(2, LoopType.Yoyo)
                .SetDelay(_delay)
                .OnComplete(()=> {
                    OuterRadiusAnimation(0);
                });
        }
    }
}
