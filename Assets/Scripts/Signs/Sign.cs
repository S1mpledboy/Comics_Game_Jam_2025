using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sign : MonoBehaviour
{
    IEnumerator corutine;
    protected float delaytime;
    protected CharacterController _player;
    protected virtual void SignAbillity()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(_player == null)
            {
                _player = collision.gameObject.GetComponent<CharacterController>();
            }
            SignAbillity();
            
            corutine = WaitForSeconds(delaytime);
            StartCoroutine(corutine);
            transform.position = new Vector3(0f, 0f, 300f);
        }
    }
    protected void RestartCorutine()
    {
        StopCoroutine(corutine);
        StartCoroutine(corutine);
    }
    protected virtual void RevertEffectOfSign()
    {
        Destroy(gameObject);
    }
    IEnumerator WaitForSeconds(float delaytime = 3f)
    {
        yield return new WaitForSeconds(delaytime);
        RevertEffectOfSign();
    }
}
