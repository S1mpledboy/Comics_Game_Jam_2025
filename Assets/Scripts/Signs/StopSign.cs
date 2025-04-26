using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSign : Sign
{

    float reduceAmuont;
    float _playersPrevSpeed;
    private void Start()
    {
        delaytime = 3f;
    }
    protected override void SignAbillity()
    {
        if (!_player.isSlowed)
        {
            reduceAmuont = _player.currentspeed - (_player.currentspeed * 0.7f);
            _player.currentspeed -= reduceAmuont;
            _player.isSlowed = true;
        }else if (_player.isSlowed)
        {
            return;
        }

    }
    protected override void RevertEffectOfSign()
    {
        if (_playersPrevSpeed != _player.currentspeed)
            _player.currentspeed += reduceAmuont;
        base.RevertEffectOfSign();
    }

}
