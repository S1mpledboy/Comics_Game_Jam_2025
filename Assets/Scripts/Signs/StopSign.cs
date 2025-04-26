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
        if(_player == null) return;
        if (!_player.isSlowed)
        {
            _playersPrevSpeed = _player.currentspeed;
            
            _player.currentspeed = _player.currentspeed*0.5f;
            print(_player.currentspeed);
            _player.isSlowed = true;
        }else if (_player.isSlowed)
        {
            Destroy(gameObject);
           return;
           
        }

    }
    protected override void RevertEffectOfSign()
    {
        _player.currentspeed = _playersPrevSpeed;
        GameObject scoreNumber = Instantiate(scoreOnBoardOb, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        scoreNumber.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(250, 3, 2);
        scoreNumber.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "-10";
        CharacterController.score -= 10f;
        Destroy(scoreNumber, 1f);
        Destroy(gameObject, 1f);
    }

}
