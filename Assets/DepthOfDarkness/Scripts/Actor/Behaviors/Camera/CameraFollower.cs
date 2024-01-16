using Unity.Netcode;
using UnityEngine;

namespace DD.Game {
    public class CameraFollower : MonoBehaviour, ILifecycleListener {
        private Transform mTarget;
        
        const float mCameraSpeed = 30f; 

        void ILifecycleListener.OnStart() {
            // TODO: don't use singlton here. mTarget may be something other then player 
            mTarget = NetworkManager.Singleton.LocalClient.PlayerObject.transform;
        }

        void ILifecycleListener.OnUpdate() {
            if (!mTarget)
                return;
            
            Vector3 direction = mCameraSpeed * Time.deltaTime * (mTarget.position - transform.position).normalized;
            direction.z = 0;
            transform.position += direction;
        } 
    }
}