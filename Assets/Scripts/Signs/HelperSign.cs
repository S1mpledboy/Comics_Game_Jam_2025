using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperSign : Sign
{

    protected override void SignAbillity()
    {
        CharacterController.score += 5f;
        base.RevertEffectOfSign();
    }
}
