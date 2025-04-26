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
        reduceAmuont = _player.currentspeed - (_player.currentspeed * 0.7f);
        _player.currentspeed -= reduceAmuont;
    }
    protected override void RevertEffectOfSign()
    {
        if (_playersPrevSpeed != _player.currentspeed)
            _player.currentspeed += reduceAmuont;
    }

}
