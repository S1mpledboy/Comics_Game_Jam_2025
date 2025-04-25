using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sign : MonoBehaviour
{
    IEnumerator corutine;
    protected float delaytime;
    protected virtual void SignAbillity(CharacterController player)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SignAbillity(collision.gameObject.GetComponent<CharacterController>());
            corutine = WaitForSeconds(delaytime);
            StartCoroutine(corutine);
        }
    }
    protected virtual void RevertEffectOfSign()
    {

    }
    IEnumerator WaitForSeconds(float delaytime = 3f)
    {
        yield return new WaitForSeconds(delaytime);
        print("Coroutine ended: " + Time.time + " seconds");
        RevertEffectOfSign();
    }
}
