using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class Sign : MonoBehaviour
{
    IEnumerator corutine;
    protected float delaytime;
    protected CharacterController _player;
    [SerializeField] protected GameObject scoreOnBoardOb;
    Vector3 cureentpos;
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
            cureentpos = transform.position;
            transform.position = new Vector3(0f, 0f, 300f);
            GameObject scoreNumber = Instantiate(scoreOnBoardOb, new Vector3(cureentpos.x, cureentpos.y + 1, cureentpos.z), Quaternion.identity);
            scoreNumber.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
            scoreNumber.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 5f.ToString();
            CharacterController.score += 5f;
            Destroy(scoreNumber, 1f);
        }
    }
    protected virtual void RevertEffectOfSign()
    {

        Destroy(gameObject, 3f);
    }
    IEnumerator WaitForSeconds(float delaytime = 3f)
    {
        yield return new WaitForSeconds(delaytime);
        RevertEffectOfSign();
    }
}
