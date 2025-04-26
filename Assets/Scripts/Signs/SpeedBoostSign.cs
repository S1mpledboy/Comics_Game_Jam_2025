using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class SpeedBoostSign : Sign
{
    float boostAmount = 5f;
    private void Start()
    {
        delaytime = 3f;
    }
    protected override void SignAbillity()
    {
        if (!_player.isSpeedBoosted)
        {
            _player.currentspeed += boostAmount;
            if (_player.currentspeed > 15f)
            {
                _player.currentspeed = 15f;
            }
            _player.isSpeedBoosted = true;
        }
        else if (_player.isSpeedBoosted)
        {
            RestartCorutine();
        }

    }
    protected override void RevertEffectOfSign()
    {
        if (_player.isSpeedBoosted)
        {
            _player.currentspeed -= boostAmount;
            _player.isSpeedBoosted = false;
        }
        base.RevertEffectOfSign();
    }
}
