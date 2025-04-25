using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSign : Sign
{
    CharacterController _player;
    float reduceAmuont;
    private void Start()
    {
        delaytime = 3f;
    }
    protected override void SignAbillity(CharacterController player)
    {
        reduceAmuont = player.currentspeed - (player.currentspeed * 0.7f);
        player.currentspeed = player.currentspeed*0.7f;
        _player = player;
        transform.position = new Vector3(0,0,300f);

    }
    protected override void RevertEffectOfSign()
    {
        _player.currentspeed += reduceAmuont;
    }

}
