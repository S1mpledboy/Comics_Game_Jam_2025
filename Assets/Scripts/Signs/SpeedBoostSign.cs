using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SpeedBoostSign : Sign
{
    float boostAmount = 5f;
    float _playersPrevSpeed;
    private void Start()
    {
        delaytime = 3f;
    }
    protected override void SignAbillity()
    {
        _playersPrevSpeed = _player.currentspeed;
        _player.currentspeed += boostAmount;
        if(_player.currentspeed > 15f)
        {
            _player.currentspeed = 15f;
        }
    }
    protected override void RevertEffectOfSign()
    {
        if(_playersPrevSpeed!=_player.currentspeed)
        _player.currentspeed -= boostAmount;
        base.RevertEffectOfSign();
    }
}
