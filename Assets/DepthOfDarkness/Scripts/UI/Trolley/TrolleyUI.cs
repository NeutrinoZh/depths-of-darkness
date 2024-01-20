using TMPro;

using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    [RequireComponent(typeof(TrolleyState))]
    public class TrolleyUI : MonoBehaviour {

        //======================================================//
        // Conts 

        const string c_canvasName = "Canvas";
        const string c_oreCountLabelName = "OreCountLabel";

        //======================================================//
        // Members 

        private string m_oreCountLabelFormat = "";

        private TextMeshProUGUI m_oreCountLabel = null;
        private TrolleyState m_trolleyState = null;

        //======================================================//
        // Lifecycles 

        private void Awake() {
            var canvas = transform.Find(c_canvasName);
            Assert.AreNotEqual(canvas, null);

            var oreCount = canvas.Find(c_oreCountLabelName);
            Assert.AreNotEqual(oreCount, null);

            m_oreCountLabel = oreCount.GetComponent<TextMeshProUGUI>();
            Assert.AreNotEqual(m_oreCountLabel, null);

            m_trolleyState = GetComponent<TrolleyState>();
            Assert.AreNotEqual(m_trolleyState, null);

            m_oreCountLabelFormat = m_oreCountLabel.text;
            m_trolleyState.OnChangeOreCount += ChangeOreCountHandle;
            ChangeOreCountHandle();
        }

        private void OnDestroy() {
            m_trolleyState.OnChangeOreCount -= ChangeOreCountHandle;
        }

        //======================================================//
        // Handles 

        private void ChangeOreCountHandle() {
            m_oreCountLabel.text = m_oreCountLabelFormat.Replace("{n}", m_trolleyState.OreCount.ToString());
        }
    }
}