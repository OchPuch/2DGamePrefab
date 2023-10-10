using System;
using UnityEngine;

namespace _2D_Simple_Mobile_Starter_pack.Scripts.CameraStuff
{
    public class CameraRatioScaler : MonoBehaviour
    {
        private Camera cam;
        public Vector2 targetResolution = new Vector2(9, 16);

        [ContextMenu("Get Aspect Ratio From Canvas Scaler")]

        public void GetAspectRatioFromCanvasScaler()
        {
            var canvasScaler = FindObjectOfType<UnityEngine.UI.CanvasScaler>();
            if (canvasScaler)
            {
                targetResolution = new Vector2(canvasScaler.referenceResolution.x, canvasScaler.referenceResolution.y);
            }
            else
            {
                Debug.LogError("No Canvas Scaler Found");
            }
        }

        private void Awake()
        {
            cam = Camera.main;
            float targetAspect = targetResolution.x / targetResolution.y;
            float windowAspect = (float)Screen.width / (float)Screen.height;
            float scaleHeight = targetAspect / windowAspect;
            cam.orthographicSize *= scaleHeight;
        }
    }
}