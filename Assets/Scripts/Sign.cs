using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sign : MonoBehaviour
{
    protected virtual void SignAbillity(CharacterController player)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SignAbillity(collision.gameObject.GetComponent<CharacterController>());
        }
    }
}
