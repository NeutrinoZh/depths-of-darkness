using UnityEngine;
using UnityEngine.Assertions;

namespace DD.Game {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    public sealed class MovementController : MonoBehaviour {

        //=====================================================//
        // Props 

        public MovementState State { get; private set; }

        //=====================================================//
        // Members 

        private Rigidbody2D m_rd;
        private PlayerInput m_input;

        //=====================================================//
        // Lifecycle 

        private void Awake() {
            State = new MovementState();

            m_rd = GetComponent<Rigidbody2D>();
            Assert.AreNotEqual(m_rd, null);

            m_input = GetComponent<PlayerInput>();
            Assert.AreNotEqual(m_input, null);
        }

        private void Update() {
            // read input value
            Vector2 direction = m_input.Input.Player.Move.ReadValue<Vector2>();

            // move
            Move(direction);

            // switch states
            State.IsMove = direction != Vector2.zero;
            if (State.IsMove)
                State.Direction = DirectionUtils.Get4DirectionFromVector(direction);
        }

        //=====================================================//
        // Internal 

        private void Move(Vector2 _direction) {
            Vector3 velocity =
                _direction * State.MoveSpeed;
            m_rd.velocity = velocity;
        }
    }
}