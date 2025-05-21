using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StopSign : Sign
{
    float _playersPrevSpeed;
    private float _slowDownVal = 3f;
    private void Start()
    {
        delaytime = 3f;
        addedSCore = -10f;
    }
    protected override void SignAbillity()
    {
        if (!_player.isSppedDown)
        {
            _player.currentspeed -= _slowDownVal;
            _player.isSppedDown = true;
        }
    }

    protected override void RevertEffectOfSign()
    {
        if (_player.isSppedDown)
        {
            FindObjectOfType<CharacterController>().currentspeed += _slowDownVal;
            GameObject scoreNumber = Instantiate(scoreOnBoardOb, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            scoreNumber.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "-10";
            CharacterController.score -= 10f;
            Destroy(scoreNumber, 1f);
            Destroy(gameObject, 1f);
            _player.isSppedDown = false;
        }
    }

    protected override IEnumerator WaitForSeconds(float delaytime = 3)
    {
        _player.slowTime = delaytime;
        while (_player.slowTime > 0f)
        {
            _player.slowTime -= Time.deltaTime;
            yield return null;
        }
        _player.slowTime = 0f;
        RevertEffectOfSign();
    }

}
