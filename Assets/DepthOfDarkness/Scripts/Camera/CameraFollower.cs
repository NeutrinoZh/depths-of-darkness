using UnityEngine;

namespace DD.Game {
    public class CameraFollower : MonoBehaviour, ILifecycleListener {
        [SerializeField] private Transform mTarget;
        
        const float mCameraSpeed = 30f; 

        void ILifecycleListener.OnUpdate() {
            Vector3 direction = (mTarget.position - transform.position).normalized * mCameraSpeed * Time.deltaTime;
            direction.z = 0;
            transform.position += direction;
        } 
    }
}