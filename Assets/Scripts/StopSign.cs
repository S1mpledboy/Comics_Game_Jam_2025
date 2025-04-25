using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSign : Sign
{
    CharacterController _player;
    float _amountOfSlowDown;
    protected override void SignAbillity(CharacterController player)
    {
        _player = player;
        _amountOfSlowDown = _player.currentspeed * 0.3f;
        print(_player.currentspeed);
        _player.SetSpeed(_player.currentspeed - _amountOfSlowDown);
        print(_player.currentspeed * 0.3f);
        print(_player.currentspeed - _amountOfSlowDown);
        print(_player.currentspeed);
        Invoke(nameof(RenewPlayerSpeed), 3f);
        transform.position = new Vector3(0,0,300f);
        print("newspeed");
    }
    void RenewPlayerSpeed()
    {
        
        _player.SetSpeed(_player.currentspeed + _amountOfSlowDown);
    }

}
