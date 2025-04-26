using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSign : Sign
{
    // Start is called before the first frame update
    void Start()
    {
        delaytime = 1f;
        addedSCore = 10f;
    }

    protected override void SignAbillity()
    {
        CharacterController.OnTakeDamage(1);
    }
    protected override void RevertEffectOfSign()
    {
        base.RevertEffectOfSign();
    }
}
