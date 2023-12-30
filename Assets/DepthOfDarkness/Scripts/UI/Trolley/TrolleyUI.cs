using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    [RequireComponent(typeof(TrolleyState))]
    public class TrolleyUI : MonoBehaviour, ILifecycleListener {
        private const string mCanvasName = "Canvas"; 
        private const string mOreCountLabelName = "OreCountLabel";
        
        private string mOreCountLabelFormat = "";

        private TextMeshProUGUI mOreCountLabel;
        private TrolleyState mTrolleyState;

        void ILifecycleListener.OnStart() {
            mOreCountLabel = transform.Find(mCanvasName)?.Find(mOreCountLabelName)?.GetComponent<TextMeshProUGUI>();
            Assert.AreNotEqual(mOreCountLabel, null);
            mOreCountLabelFormat = mOreCountLabel.text;

            mTrolleyState = GetComponent<TrolleyState>();
            Assert.AreNotEqual(mTrolleyState, null);
        
            mTrolleyState.ChangeOreCountHandle += ChangeOreCountHandle;
            ChangeOreCountHandle();
        }

        void ILifecycleListener.OnFinish() {
            mTrolleyState.ChangeOreCountHandle -= ChangeOreCountHandle;
        }

        private void ChangeOreCountHandle() {
            mOreCountLabel.text = mOreCountLabelFormat.Replace("{n}", mTrolleyState.OreCount.ToString());
        }
    }
}