using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSign : Sign
{
    protected override void SignAbillity(CharacterController player)
    {
        player.SetSpeed(player._speed * 0.7f);
        gameObject.SetActive(false);
    }
}
