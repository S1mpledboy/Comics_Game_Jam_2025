using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperSign : Sign
{

    protected override void SignAbillity(CharacterController player)
    {
        player._helperSignsAmount++;
        player.UpdateHelpersSign();
    }
}
