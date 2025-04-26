using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : Attack
{

    protected override void Awake()
    {
        chargeTime = 1f; // how long to charge
        radius = 5f; // how far from player
        attackRadius = 2f; // how big area damage

        base.Awake();
    }
}
