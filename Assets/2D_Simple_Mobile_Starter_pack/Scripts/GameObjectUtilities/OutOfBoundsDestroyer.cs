using UnityEngine;

namespace _2D_Simple_Mobile_Starter_pack.Scripts.GameObjectUtilities
{
    public class OutOfBoundsDestroyer : MonoBehaviour
    {
        public string[] tagsToDestroy;
        private void OnTriggerExit2D(Collider2D other)
        {
            if (tagsToDestroy.Length == 0) return;
            foreach (var t in tagsToDestroy)
            {
                if (other.CompareTag(t))
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
