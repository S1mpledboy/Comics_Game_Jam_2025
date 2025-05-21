using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class SpeedBoostSign : Sign
{
    float _playersPrevSpeed;
    private float _bonusSpeed = 3f;
    private void Start()
    {
        delaytime = 3f;
        addedSCore = 10;

    }
    protected override void SignAbillity()
    {
        if (!_player.isSppedUp)
        {
            _player.currentspeed += _bonusSpeed;
            _player.isSppedUp = true;
        }
    }
    protected override void RevertEffectOfSign()
    {
        if (_player.isSppedUp)
        {
            _player.currentspeed -= _bonusSpeed;
            base.RevertEffectOfSign();
            _player.isSppedUp = false;

        }
    }

    protected override IEnumerator WaitForSeconds(float delaytime = 3)
    {
        _player.boostTime = delaytime;
        while (_player.boostTime > 0f)
        {
            _player.boostTime -= Time.deltaTime;
            yield return null;
        }
        _player.boostTime = 0f;
        RevertEffectOfSign();
    }
}
