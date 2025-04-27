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
         
        _player.currentspeed = 10f;

    }
    protected override void RevertEffectOfSign()
    {

        FindObjectOfType<CharacterController>().currentspeed = 7;

        base.RevertEffectOfSign();
    }
}
