using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SequenceCircleAttack : CircleAttack
{
    protected int numberOfAttacks = 6;
    protected override void Awake()
    {
        chargeTime = 1f; // how long to charge
        radius = 5f; // how far from player
        attackRadius = 2f; // how big area damage

        spriteRenderer = GetComponent<SpriteRenderer>();


        playerGo = FindObjectOfType<CharacterController>().gameObject;
        mapBounds = mapGoAttack.GetComponent<SpriteRenderer>().bounds;
        playerPos = playerGo.transform.position;

        // set position
        transform.position = CalculatePosition();
        StartAttacking();

        for(int i = 1; i < numberOfAttacks; i++)
        {

        }
    }
}
