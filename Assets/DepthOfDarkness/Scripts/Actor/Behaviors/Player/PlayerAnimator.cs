using UnityEngine;
using UnityEngine.Assertions;

using System.Collections.Generic;

namespace DD.Game {
    public sealed class PlayerAnimator : MonoBehaviour {

        //=====================================================//
        // Members 

        private MovementState m_movement;

        private Dictionary<Direction, int> m_animations;

        private Animator m_animator;

        //===============================================================//
        // Lifecycles 

        private void Awake() {
            //

            m_movement = GetComponent<MovementController>()?.State;
            Assert.AreNotEqual(m_movement, null);

            m_animator = GetComponent<Animator>();
            Assert.AreNotEqual(m_animator, null);

            // 

            m_animator.speed = 0;
            m_animations = new() {

                { Direction.UP,        Animator.StringToHash($"Move{Direction.UP.ToPrettyString()}")        },
                { Direction.LEFT,      Animator.StringToHash($"Move{Direction.LEFT.ToPrettyString()}")      },
                { Direction.DOWN,      Animator.StringToHash($"Move{Direction.DOWN.ToPrettyString()}")      },
                { Direction.RIGHT,     Animator.StringToHash($"Move{Direction.RIGHT.ToPrettyString()}")     },

                // { Direction.UPRIGHT,   Animator.StringToHash($"Move{Direction.UPRIGHT.ToPrettyString()}")   },
                // { Direction.UPLEFT,    Animator.StringToHash($"Move{Direction.UPLEFT.ToPrettyString()}")    },
                // { Direction.DOWNLEFT,  Animator.StringToHash($"Move{Direction.DOWNLEFT.ToPrettyString()}")  },
                // { Direction.DOWNRIGHT, Animator.StringToHash($"Move{Direction.DOWNRIGHT.ToPrettyString()}") },
            };

            //

            m_movement.OnChangeDirection += OnChangeDirectionHandle;
            m_movement.OnChangeMoveState += OnChangeMoveStateHandle;

        }

        private void OnDestroy() {
            m_movement.OnChangeDirection -= OnChangeDirectionHandle;
            m_movement.OnChangeMoveState -= OnChangeMoveStateHandle;
        }

        //===============================================================//
        // Handles 

        void OnChangeDirectionHandle() {
            m_animator.Play(m_animations[m_movement.Direction]);
        }

        void OnChangeMoveStateHandle() {
            m_animator.speed = m_movement.IsMove ? 1 : 0;
        }

        //===============================================================//

    }
}