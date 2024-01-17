using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    public sealed class MovementController : MonoBehaviour, ILifecycleListener {
        private MovementState m_State;
        public MovementState State => m_State;

        private Rigidbody2D m_Rd;
        private PlayerInput m_Input;

        void ILifecycleListener.OnInit() {
            m_State = new MovementState();
        }

        void ILifecycleListener.OnStart() {
            m_Rd = GetComponent<Rigidbody2D>();
            Assert.AreNotEqual(m_Rd, null);

            m_Input = GetComponent<PlayerInput>();
            Assert.AreNotEqual(m_Input, null);
        }

        void ILifecycleListener.OnUpdate() {
            // read input value
            Vector2 direction = m_Input.Input.Player.Move.ReadValue<Vector2>();

            // move
            Move(direction);

            // switch states
            m_State.IsMove = direction != Vector2.zero;
            if (m_State.IsMove)
                m_State.Direction = DirectionUtils.GetDirectionFromVector(direction);
        }

        private void Move(Vector2 _direction) {
            Vector3 velocity =
                _direction * m_State.MoveSpeed;
            m_Rd.velocity = velocity;
        }
    }
}