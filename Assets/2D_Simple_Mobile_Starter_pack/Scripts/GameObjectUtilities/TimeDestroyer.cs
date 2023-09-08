using UnityEngine;

namespace _2D_Simple_Mobile_Starter_pack.Scripts.GameObjectUtilities
{
    public class TimeDestroyer : MonoBehaviour
    {
        
        [SerializeField] private float timeToDestroy;
        [SerializeField] private bool activateOnStart;
        private bool activated;
        private void Start()
        {
            if (activateOnStart)
            {
                Activate();
            }
        }
        
        public void Activate()
        {
            if (activated) return;
            if (timeToDestroy <= 0) return;
            Destroy(gameObject, timeToDestroy);
            activated = true;
        }
        
        public void Activate(float time)
        {
            timeToDestroy = time;
            Activate();
        }
        
    }
}
