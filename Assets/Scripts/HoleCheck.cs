using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hole"))
        {
            Destroy(collision.gameObject);
        }
    }
}
