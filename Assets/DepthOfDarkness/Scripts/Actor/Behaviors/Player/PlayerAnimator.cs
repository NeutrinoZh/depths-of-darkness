using UnityEngine;
using UnityEngine.Assertions;

using System.Collections.Generic;

namespace DD.Game {
    public sealed class PlayerAnimator : MonoBehaviour {

        //=====================================================//
        // Members 

        private GrabController m_grabController;
        private MovementState m_movement;


        private Dictionary<Direction, int> m_anims_move;
        private Dictionary<Direction, int> m_anims_idle;
        private Dictionary<Direction, int> m_anims_move_lamp;
        private Dictionary<Direction, int> m_anims_idle_lamp;

        private Animator m_animator;

        //===============================================================//
        // Lifecycles 

        private void Awake() {
            //

            m_grabController = GetComponent<GrabController>();
            Assert.IsNotNull(m_grabController);

            m_movement = GetComponent<MovementState>();
            Assert.IsNotNull(m_movement);

            m_animator = GetComponent<Animator>();
            Assert.IsNotNull(m_animator);

            // 

            m_anims_move = new() {
                { Direction.UP,        Animator.StringToHash($"Move{Direction.UP.ToPrettyString()}")        },
                { Direction.LEFT,      Animator.StringToHash($"Move{Direction.LEFT.ToPrettyString()}")      },
                { Direction.DOWN,      Animator.StringToHash($"Move{Direction.DOWN.ToPrettyString()}")      },
                { Direction.RIGHT,     Animator.StringToHash($"Move{Direction.RIGHT.ToPrettyString()}")     },

                { Direction.UPRIGHT,   Animator.StringToHash($"Move{Direction.UPRIGHT.ToPrettyString()}")   },
                { Direction.UPLEFT,    Animator.StringToHash($"Move{Direction.UPLEFT.ToPrettyString()}")    },
                { Direction.DOWNLEFT,  Animator.StringToHash($"Move{Direction.DOWNLEFT.ToPrettyString()}")  },
                { Direction.DOWNRIGHT, Animator.StringToHash($"Move{Direction.DOWNRIGHT.ToPrettyString()}") },
            };

            m_anims_idle = new() {
                { Direction.UP,        Animator.StringToHash($"Idle{Direction.UP.ToPrettyString()}")        },
                { Direction.LEFT,      Animator.StringToHash($"Idle{Direction.LEFT.ToPrettyString()}")      },
                { Direction.DOWN,      Animator.StringToHash($"Idle{Direction.DOWN.ToPrettyString()}")      },
                { Direction.RIGHT,     Animator.StringToHash($"Idle{Direction.RIGHT.ToPrettyString()}")     },

                { Direction.UPRIGHT,   Animator.StringToHash($"Idle{Direction.UPRIGHT.ToPrettyString()}")   },
                { Direction.UPLEFT,    Animator.StringToHash($"Idle{Direction.UPLEFT.ToPrettyString()}")    },
                { Direction.DOWNLEFT,  Animator.StringToHash($"Idle{Direction.DOWNLEFT.ToPrettyString()}")  },
                { Direction.DOWNRIGHT, Animator.StringToHash($"Idle{Direction.DOWNRIGHT.ToPrettyString()}") },
            };

            m_anims_move_lamp = new() {
                { Direction.UP,        Animator.StringToHash($"MoveLamp{Direction.UP.ToPrettyString()}")        },
                { Direction.LEFT,      Animator.StringToHash($"MoveLamp{Direction.LEFT.ToPrettyString()}")      },
                { Direction.DOWN,      Animator.StringToHash($"MoveLamp{Direction.DOWN.ToPrettyString()}")      },
                { Direction.RIGHT,     Animator.StringToHash($"MoveLamp{Direction.RIGHT.ToPrettyString()}")     },

                { Direction.UPRIGHT,   Animator.StringToHash($"MoveLamp{Direction.UPRIGHT.ToPrettyString()}")   },
                { Direction.UPLEFT,    Animator.StringToHash($"MoveLamp{Direction.UPLEFT.ToPrettyString()}")    },
                { Direction.DOWNLEFT,  Animator.StringToHash($"MoveLamp{Direction.DOWNLEFT.ToPrettyString()}")  },
                { Direction.DOWNRIGHT, Animator.StringToHash($"MoveLamp{Direction.DOWNRIGHT.ToPrettyString()}") },
            };

            m_anims_idle_lamp = new() {
                { Direction.UP,        Animator.StringToHash($"IdleLamp{Direction.UP.ToPrettyString()}")        },
                { Direction.LEFT,      Animator.StringToHash($"IdleLamp{Direction.LEFT.ToPrettyString()}")      },
                { Direction.DOWN,      Animator.StringToHash($"IdleLamp{Direction.DOWN.ToPrettyString()}")      },
                { Direction.RIGHT,     Animator.StringToHash($"IdleLamp{Direction.RIGHT.ToPrettyString()}")     },

                { Direction.UPRIGHT,   Animator.StringToHash($"IdleLamp{Direction.UPRIGHT.ToPrettyString()}")   },
                { Direction.UPLEFT,    Animator.StringToHash($"IdleLamp{Direction.UPLEFT.ToPrettyString()}")    },
                { Direction.DOWNLEFT,  Animator.StringToHash($"IdleLamp{Direction.DOWNLEFT.ToPrettyString()}")  },
                { Direction.DOWNRIGHT, Animator.StringToHash($"IdleLamp{Direction.DOWNRIGHT.ToPrettyString()}") },
            };

            //

            m_movement.OnChangeDirection += ChangeAnimation;
            m_movement.OnChangeMoveState += ChangeAnimation;
            m_grabController.OnGrab += OnGrabDropHandle;
            m_grabController.OnDrop += OnGrabDropHandle;

        }

        private void OnDestroy() {
            m_movement.OnChangeDirection -= ChangeAnimation;
            m_movement.OnChangeMoveState -= ChangeAnimation;
            m_grabController.OnGrab -= OnGrabDropHandle;
            m_grabController.OnDrop -= OnGrabDropHandle;
        }

        //===============================================================//
        // Handles 

        void OnGrabDropHandle(Pickable _) {
            ChangeAnimation();
        }

        void ChangeAnimation() {
            if (m_movement.IsStay)
                m_animator.Play(m_grabController.Grabbed ? m_anims_idle_lamp[m_movement.Direction] : m_anims_idle[m_movement.Direction]);
            else
                m_animator.Play(m_grabController.Grabbed ? m_anims_move_lamp[m_movement.Direction] : m_anims_move[m_movement.Direction]);
        }

        //===============================================================//

    }
}