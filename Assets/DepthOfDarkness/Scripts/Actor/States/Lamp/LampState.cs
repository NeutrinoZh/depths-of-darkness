using System;

using UnityEngine;

namespace DD.Game {
    public class LampState : MonoBehaviour {
        public Action<EOilLevel> OnOilLevelChange = null;

        [field: SerializeField] public float MaxOil { get; private set; }
        [field: SerializeField] public float CurrentOil { get; private set; }
        [field: SerializeField] public float OilUsage { get; private set; }
        [field: SerializeField] public EOilLevel OilLevel { get; private set; }

        public enum EOilLevel : int {
            Full,
            HalfFull,
            QuarterFull,
            Empty
        }

        public void CheckCurrentOil() {
            switch (CurrentOil) {

                case >= 0.5f:
                    ChangeOilLevel(EOilLevel.Full);
                    break;

                case >= 0.25f:
                    ChangeOilLevel(EOilLevel.HalfFull);
                    break;

                case >= 0.01f:
                    ChangeOilLevel(EOilLevel.QuarterFull);
                    break;

                case <= 0f:
                    ChangeOilLevel(EOilLevel.Empty);
                    break;

                default:
                    break;
            }
        }

        private void ChangeOilLevel(EOilLevel _oilLevel) {
            if (OilLevel == _oilLevel) return;

            OilLevel = _oilLevel;
            OnOilLevelChange?.Invoke(OilLevel);
        }
    }
}