using UnityEngine;

namespace DD.Game {
    public class CameraFollower : MonoBehaviour, ILifecycleListener {
        [SerializeField] private Transform mTarget;
        
        const float mCameraSpeed = 30f; 

        void ILifecycleListener.OnUpdate() {
            if (!mTarget)
                return;
            
            Vector3 direction = mCameraSpeed * Time.deltaTime * (mTarget.position - transform.position).normalized;
            direction.z = 0;
            transform.position += direction;
        } 
    }
}