using UnityEngine;
using UnityEngine.Assertions;

using System.Collections.Generic;

namespace DD.Game {
    public class PlayerAnimator : MonoBehaviour, ILifecycleListener {
        private MovementState mMovement;

        private Dictionary<Direction, int> mAnimations;
        private Animator mAnimator;

        //===============================================================//

        void ILifecycleListener.OnStart() {
            //===============================================================//
            // Get components 

            mMovement = GetComponent<MovementController>()?.State;
            Assert.AreNotEqual(mMovement, null);

            mAnimator = GetComponent<Animator>();
            Assert.AreNotEqual(mAnimator, null);

            //===============================================================//
            // Setup 
            
            mAnimations = new() {
                { Direction.UPRIGHT,   Animator.StringToHash($"Move{Direction.UPRIGHT.ToPrettyString()}")   },
                { Direction.UP,        Animator.StringToHash($"Move{Direction.UP.ToPrettyString()}")        },
                { Direction.UPLEFT,    Animator.StringToHash($"Move{Direction.UPLEFT.ToPrettyString()}")    },
                { Direction.LEFT,      Animator.StringToHash($"Move{Direction.LEFT.ToPrettyString()}")      },
                { Direction.DOWNLEFT,  Animator.StringToHash($"Move{Direction.DOWNLEFT.ToPrettyString()}")  },
                { Direction.DOWN,      Animator.StringToHash($"Move{Direction.DOWN.ToPrettyString()}")      },
                { Direction.DOWNRIGHT, Animator.StringToHash($"Move{Direction.DOWNRIGHT.ToPrettyString()}") },
                { Direction.RIGHT,     Animator.StringToHash($"Move{Direction.RIGHT.ToPrettyString()}")     },
            };

            mAnimator.speed = 0;

            //===============================================================//
            // Registers callbacks

            mMovement.OnChangeDirection += OnChangeDirectionHandle;
            mMovement.OnChangeMoveState += OnChangeMoveStateHandle;

            //===============================================================//
        }

        void ILifecycleListener.OnFinish() {
            mMovement.OnChangeDirection -= OnChangeDirectionHandle;
            mMovement.OnChangeMoveState -= OnChangeMoveStateHandle;
        }

        //===============================================================//

        void OnChangeDirectionHandle() {
            mAnimator.Play(mAnimations[mMovement.Direction]);
        }

        void OnChangeMoveStateHandle() {
            mAnimator.speed = mMovement.IsMove ? 1 : 0;
        }
    }
}