using Unity.Netcode;

using UnityEngine;

namespace DD.Game {
    public class CameraFollower : MonoBehaviour {

        //=============================================//
        // Consts 

        const float c_cameraSpeed = 30f;

        //=============================================//
        // Members 

        private Transform m_target;

        //=============================================//
        // Props 

        private void Awake() {
            // FIXME: don't use singlton here. mTarget may be something other then player 
            m_target = NetworkManager.Singleton.LocalClient.PlayerObject.transform;
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
    }
}