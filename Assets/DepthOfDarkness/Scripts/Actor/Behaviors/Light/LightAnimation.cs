using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

namespace DD.Game {
    [RequireComponent(typeof(Light2D))]
    public class LightAnimation : MonoBehaviour {
        [SerializeField] private Ease mEase;

        [SerializeField] private float mMinIntensity;
        [SerializeField] private float mIntensityAnimDuration;

        [SerializeField] private float mMinOuterRadius;
        [SerializeField] private float mOuterRadiusAnimDuration;

        private Light2D mLight = null;

        private void Awake() {
            mLight = GetComponent<Light2D>();
            Assert.AreNotEqual(mLight, null);

            DOTween
                .To(
                    () => mLight.intensity,
                    x => mLight.intensity = x,
                    mMinIntensity,
                    mIntensityAnimDuration
                )
                .SetEase(mEase)
                .SetLoops(-1, LoopType.Yoyo);

            DOTween
                .To(
                    () => mLight.pointLightOuterRadius,
                    x => mLight.pointLightOuterRadius = x,
                    mMinOuterRadius,
                    mOuterRadiusAnimDuration
                )
                .SetEase(mEase)
                .SetLoops(-1, LoopType.Yoyo);

        }
    }
}
