using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSign : Sign
{
    float _playersPrevSpeed;
    private void Start()
    {
        delaytime = 3f;
    }
    protected override void SignAbillity()
    {
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
        base.RevertEffectOfSign();
    }

}
