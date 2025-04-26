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
            transform.position = new Vector3(0, 0, 300f);
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
        RevertEffectOfSign();
    }
}
