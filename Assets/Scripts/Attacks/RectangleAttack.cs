using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RectangleAttack : Attack
{
    protected override void Awake()
    {
        chargeTime = 2f; // how long to charge
        radius = 1f; // how far from player
        attackRadius = 1f; // how big area damage

        //transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 90f));

        base.Awake();

    }

    protected override Task ChargeAttack()
    {
        //spriteRenderer.sprite = attackWarningSprite;
        transform.localScale = new Vector2(attackRadius, attackRadius * 2);
        transform.GetChild(0).localScale = new Vector2(1f, 2f);
        
        return base.ChargeAttack();
    }
    /*
    protected override void DealDamage()
    {
        
        gameObject.AddComponent<BoxCollider2D>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2 (1f, 1f);
        boxCollider.enabled = true;
        
    }
    */
}
