using UnityEngine;

namespace Script
{
    public class CameraSmoothment : MonoBehaviour
    {
        public Transform target;
        public float smoothTime = 0.3f;
        private Vector3 _velocity = Vector3.zero;

        private void LateUpdate()
        {
            if (!target) 
            {
                return;
            }

            var targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }
    }
}
