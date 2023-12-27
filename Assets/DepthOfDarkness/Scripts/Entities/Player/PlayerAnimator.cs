using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    public class PlayerAnimator : ILifecycleListener {
        
        private PlayerModel mModel;
        private Player mPlayer;

        private Dictionary<Direction, int> mAnimations;
        private Animator mAnimator;

        public PlayerAnimator(Player _player, PlayerModel _model) {
            mPlayer = _player;
            Assert.AreNotEqual(mPlayer, null);

            mModel = _model;
            Assert.AreNotEqual(mModel, null);

            mAnimator = mModel.Transform.GetComponent<Animator>();
            Assert.AreNotEqual(mAnimator, null);

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
        }

        void ILifecycleListener.OnStart() {
            mPlayer.OnDirectionChangeEvent += OnDirectionChangeHandle;
            mPlayer.OnStateChangeEvent += OnStateChangeHandle;
        }

        void ILifecycleListener.OnFinish() {
            mPlayer.OnDirectionChangeEvent -= OnDirectionChangeHandle;
            mPlayer.OnStateChangeEvent -= OnStateChangeHandle;
        }

        void OnDirectionChangeHandle() {
            mAnimator.Play(mAnimations[mModel.Direction]);
        }

        void OnStateChangeHandle() {
            mAnimator.speed = mModel.IsMove ? 1 : 0;
        }
    }
}