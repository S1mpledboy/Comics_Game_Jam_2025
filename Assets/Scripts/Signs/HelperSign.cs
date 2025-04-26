using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperSign : Sign
{

    protected override void SignAbillity()
    {
        _player._helperSignsAmount++;
        _player.UpdateHelpersSign();
    }
}
