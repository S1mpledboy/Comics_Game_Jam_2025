using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class SpeedBoostSign : Sign
{
    float _playersPrevSpeed;
    private void Start()
    {
        delaytime = 3f;
        addedSCore = 10;
    }
    protected override void SignAbillity()
    {
        if (!_player.isSpeedBoosted)
        {
            _playersPrevSpeed = _player.currentspeed;
            _player.currentspeed = _player.currentspeed*1.5f;
            if (_player.currentspeed > 12f)
            {
                _player.currentspeed = 12f;
            }
            _player.isSpeedBoosted = true;
        }
        else if (_player.isSpeedBoosted)
        {
            return;
        }

    }
    protected override void RevertEffectOfSign()
    {
        if (_player.isSpeedBoosted)
        {
            _player.currentspeed = _playersPrevSpeed;
            _player.isSpeedBoosted = false;
        }
        base.RevertEffectOfSign();
    }
}
