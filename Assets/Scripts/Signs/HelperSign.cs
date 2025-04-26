using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperSign : Sign
{
    private void Start()
    {
        addedSCore = 5f;
    }
    protected override void SignAbillity()
    {
        
        base.RevertEffectOfSign();
    }
}
