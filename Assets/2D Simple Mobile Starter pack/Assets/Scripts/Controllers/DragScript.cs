using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 startPosition;

    private void OnMouseDown()
    {
        isDragging = true;
        startPosition = transform.position;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            if (GameManager.instance.gameState == GameManager.GameState.Setup) GameManager.instance.onGameStart?.Invoke();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z; // Maintain the original Z position

            transform.position = new Vector3(mousePosition.x, startPosition.y, mousePosition.z);
        }
    }
}