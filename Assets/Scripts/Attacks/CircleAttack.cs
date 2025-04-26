using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    protected override Task DropDownAttack()
    {
        transform.GetChild(0).localScale = new Vector2(1.5f, 1.5f);

        return base.DropDownAttack();
    }
}
