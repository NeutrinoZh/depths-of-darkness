using System;

using UnityEngine;

namespace DD.Game {
    [Serializable]
    public struct LampStateConfigauration {
        public float MaxOil;
        public float CurrentOil;
        public float OilUsage;
    }

    public class LampState : MonoBehaviour {
        //=======================================//
        // Events
        public event Action<EOilLevel> OnOilLevelChange = null;

        //=======================================//
        // Type

        public enum EOilLevel : int {
            Full,
            HalfFull,
            QuarterFull,
            Empty
        }

        //=======================================//
        // Props 

        [field: SerializeField] public EOilLevel OilLevel { get; private set; }

        [field: SerializeField] public float MaxOil { get; private set; }
        [field: SerializeField] public float CurrentOil { get; private set; }
        [field: SerializeField] public float OilUsage { get; private set; }

        //=======================================//
        // Members

        [SerializeField] private LampStateConfigauration m_lampStateConfigaration;

        //=======================================//
        // API

        public void UseOil() {
            CurrentOil -= OilUsage * Time.deltaTime;
            CurrentOil = Mathf.Clamp(CurrentOil, 0, 1);
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

        //=======================================//
        // Internal logic

        private void Awake() {
            MaxOil = m_lampStateConfigaration.MaxOil;
            CurrentOil = m_lampStateConfigaration.CurrentOil / MaxOil;
            OilUsage = m_lampStateConfigaration.OilUsage / MaxOil;
        }

        private void ChangeOilLevel(EOilLevel _oilLevel) {
            if (OilLevel == _oilLevel) return;

            OilLevel = _oilLevel;
            OnOilLevelChange?.Invoke(OilLevel);
        }
    }
}