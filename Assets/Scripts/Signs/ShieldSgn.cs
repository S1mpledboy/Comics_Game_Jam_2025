using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSgn : Sign
{
    SpriteRenderer sprite;
    private void Start()
    {
        delaytime = 3f;
    }
    protected override void SignAbillity()
    {
        _player.isShielded = true;
        _player.shield.gameObject.SetActive(true);
    }
    protected override void RevertEffectOfSign()
    {
        print("shield off");
        _player.isShielded = false;
        _player.shield.gameObject.SetActive(false);
        base.RevertEffectOfSign();
    }
}
