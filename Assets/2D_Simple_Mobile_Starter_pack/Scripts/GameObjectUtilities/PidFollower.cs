using UnityEngine;

namespace _2D_Simple_Mobile_Starter_pack.Scripts.GameObjectUtilities
{
    public class PidFollower : MonoBehaviour
    {
        public PidRegulation pidRegulation = new PidRegulation(1, 0, 0);
        [Header("Offset")]
        public bool getOffsetOnAwake;
        public Vector3 offset;
        [Header("Follow")]
        public Transform target;
        public bool followX, followY, followZ;
        [Header("Damping")]
        public float damping = 1;
        private Vector3 velocity = Vector3.zero;
        private void Start()
        {
            if (getOffsetOnAwake)
            {
                offset = transform.position - target.position;
            }
        }

        private void LateUpdate()
        {
            Vector3 targetPos = target.position + offset;
            if (!followX)
            {
                targetPos.x = transform.position.x;
            }
            if (!followY)
            {
                targetPos.y = transform.position.y;
            }
            if (!followZ)
            {
                targetPos.z = transform.position.z;
            }

            var error = Vector3.Distance(transform.position, targetPos);
            var direction = (targetPos - transform.position).normalized;
            var force = pidRegulation.GetPid(error, Time.deltaTime);
            transform.position += direction * force;
        }
    }
}