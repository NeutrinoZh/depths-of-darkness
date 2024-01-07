using System;

namespace DD.Game {
    public class MovementState : IEntityState {

        // Events

        public Action OnChangeMoveState = null;
        public Action OnChangeDirection = null;

        // Members

        private Direction mDirection = Direction.DOWN;
        private bool mIsMove = false;

        // Props 

        public float MoveSpeed => 2f;
        public bool IsStay => !IsMove;

        /// <summary>
        /// Handled in OnChangeDirection
        /// </summary>
        public Direction Direction {
            get => mDirection;
            set {
                if (mDirection == value)
                    return;

                mDirection = value;
                OnChangeDirection?.Invoke();
            }
        }

        /// <summary>
        /// Handled in OnChangeMoveState
        /// </summary>
        public bool IsMove {
            get => mIsMove;
            set {
                if (mIsMove == value)
                    return;

                mIsMove = value;
                OnChangeMoveState?.Invoke();
            }
        }
    }
}