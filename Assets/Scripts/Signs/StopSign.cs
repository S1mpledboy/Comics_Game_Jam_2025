using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StopSign : Sign
{
    float _playersPrevSpeed;
    private void Start()
    {
        delaytime = 3f;
        addedSCore = -10f;
    }
    protected override void SignAbillity()
    {

            
            _player.currentspeed -= 3f;
            print(_player.currentspeed);



    }
    protected override void RevertEffectOfSign()
    {
        FindObjectOfType<CharacterController>().currentspeed += 3;
        GameObject scoreNumber = Instantiate(scoreOnBoardOb, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        scoreNumber.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "-10";
        CharacterController.score -= 10f;
        Destroy(scoreNumber, 1f);
        Destroy(gameObject, 1f);
    }

}
