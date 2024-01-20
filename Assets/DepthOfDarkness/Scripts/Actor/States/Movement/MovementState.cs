using System;

using UnityEngine;

namespace DD.Game {
    public class MovementState : MonoBehaviour {
        //=======================================//
        // Events

        public Action OnChangeMoveState = null;
        public Action OnChangeDirection = null;

        //=======================================//
        // Props 

        [field: SerializeField] public float MoveSpeed { get; set; }
        public bool IsStay => !IsMove;

        /// <summary>
        /// Handled in OnChangeDirection
        /// </summary>
        public Direction Direction {
            get => m_direction;
            set {
                if (m_direction == value)
                    return;

                m_direction = value;
                OnChangeDirection?.Invoke();
            }
        }

        /// <summary>
        /// Handled in OnChangeMoveState
        /// </summary>
        public bool IsMove {
            get => m_isMove;
            set {
                if (m_isMove == value)
                    return;

                m_isMove = value;
                OnChangeMoveState?.Invoke();
            }
        }

        //=======================================//
        // Members

        private Direction m_direction = Direction.DOWN;
        private bool m_isMove = false;

    }
}