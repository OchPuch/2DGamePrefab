using UnityEngine;

namespace _2D_Simple_Mobile_Starter_pack.Scripts.GameObjectUtilities
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class FullScreenMapScaler : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D mapCollider2D;
        void Start()
        {
            if (!mapCollider2D) mapCollider2D = GetComponent<BoxCollider2D>();
            // Get the screen size in world coordinates and scale the map to fit
            transform.position = new Vector3(0, 0, 0);
            Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
            mapCollider2D.size = new Vector3(screenSize.x , screenSize.y , 1f);
        }

    
    }
}
