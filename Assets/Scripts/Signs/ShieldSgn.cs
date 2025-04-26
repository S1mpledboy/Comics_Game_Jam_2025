using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSgn : Sign
{
    private void Start()
    {
        delaytime = 5f;
    }
    protected override void SignAbillity()
    {
        _player.isShielded = true;
    }
    protected override void RevertEffectOfSign()
    {
        _player.isShielded = false;
    }
}
