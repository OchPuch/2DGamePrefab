using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsDestroyer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
