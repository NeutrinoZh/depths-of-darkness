using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    public class TrolleyAnimator : MonoBehaviour, ILifecycleListener {
        [SerializeField] private List<Sprite> mSprites;

        private TrolleyState mTrolleyState;
        private SpriteRenderer mRenderer;

        void ILifecycleListener.OnStart() {
            mTrolleyState = GetComponent<TrolleyState>();
            Assert.AreNotEqual(mTrolleyState, null);
       
            mRenderer = GetComponent<SpriteRenderer>();
            Assert.AreNotEqual(mRenderer, null);

            mTrolleyState.OnChangeOreCount += ChangeOreCountHandle;
        }

        void ILifecycleListener.OnFinish() {
            mTrolleyState.OnChangeOreCount -= ChangeOreCountHandle;
        }

        private void ChangeOreCountHandle() {
            // progress in percent (0-1)
            float progress = mTrolleyState.OreCount / 10f;
            
            // 
            int chunk = (int)(progress * mSprites.Count);
            if (chunk >= mSprites.Count)
                chunk = mSprites.Count - 1;

            //
            mRenderer.sprite = mSprites[chunk];
        }
    }
}