using System;

using UnityEngine;

namespace DD.Game {
    public class LampState : MonoBehaviour {
        public Action<EOilLevel> OnOilLevelChange = null;

        [field: SerializeField] public float MaxOil { get; private set; }
        [field: SerializeField] public float CurrentOil { get; private set; }
        [field: SerializeField] public float OilUsage { get; private set; }
        [field: SerializeField] public EOilLevel OilLevel { get; private set; }

        public enum EOilLevel {
            Full,
            HalfFull,
            LessHalfFull,
            Empty
        }

        public void CheckCurrentOil() {
            switch ((CurrentOil, OilLevel)) {

                case ( < 0.5f, EOilLevel.Full):
                    OilLevel = EOilLevel.HalfFull;
                    OnOilLevelChange?.Invoke(OilLevel);
                    break;

                case ( < 0.2f, EOilLevel.HalfFull):
                    OilLevel = EOilLevel.LessHalfFull;
                    OnOilLevelChange?.Invoke(OilLevel);
                    break;

                case ( <= 0f, EOilLevel.LessHalfFull):
                    OilLevel = EOilLevel.Empty;
                    OnOilLevelChange?.Invoke(OilLevel);
                    break;

                default:
                    break;
            }
        }

    }
}

