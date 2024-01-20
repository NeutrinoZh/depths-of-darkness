using Unity.Netcode;

using UnityEngine;

using Zenject;

namespace DD.Game {
    public class CameraFollower : MonoBehaviour {

        //=============================================//
        // Consts 

        const float c_cameraSpeed = 30f;

        //=============================================//
        // Members 

        private Transform m_target;

        //=============================================//
        // Lifecycle 

        [Inject]
        public void Construct(PlayerProxy _playerProxy) {
            _playerProxy.OnSelfConnect += OnPlayerConnectHandle;
        }


        private void Update() {
            if (!m_target)
                return;

            Vector3 direction =
                c_cameraSpeed * Time.deltaTime *
                (m_target.position - transform.position).normalized;
            direction.z = 0;

            transform.position += direction;
        }

        //=============================================//
        // Handlers

        private void OnPlayerConnectHandle(Transform _transformPlayer) {
            m_target = _transformPlayer;
        }
    }
}