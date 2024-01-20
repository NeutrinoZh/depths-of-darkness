using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(MovementState))]
    public sealed class MovementController : MonoBehaviour {
        //=====================================================//
        // Members 

        private MovementState m_movementState;
        private Rigidbody2D m_rd;
        private PlayerInput m_input;

        //=====================================================//
        // Lifecycle 

        private void Awake() {
            m_movementState = GetComponent<MovementState>();
            Assert.AreNotEqual(m_movementState, null);

            m_rd = GetComponent<Rigidbody2D>();
            Assert.AreNotEqual(m_rd, null);

            m_input = GetComponent<PlayerInput>();
            Assert.AreNotEqual(m_input, null);
        }

        private void FixedUpdate() {
            // read input value
            Vector2 direction = m_input.Input.Player.Move.ReadValue<Vector2>();

            // move
            Move(direction);

            // switch states
            m_movementState.IsMove = direction != Vector2.zero;
            if (m_movementState.IsMove)
                m_movementState.Direction = DirectionUtils.Get4DirectionFromVector(direction);
        }

        //=====================================================//
        // Internal 

        private void Move(Vector2 _direction) {
            Vector3 velocity =
                m_movementState.MoveSpeed * _direction;
            m_rd.velocity = velocity;
        }
    }
}