using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Attack : MonoBehaviour
{
    /*
    cykl ¿ycia ataku:
    1)  spawnuje siê w okolicy gracza
    2)  pokazuje siê na pod³odze obszar zagro¿enia
        z czasem robi siê co raz bardziej czerwony
    3)  po czasie ³adowania siê odpala
    4)  usuniêcie ze sceny
   */
    protected GameObject playerGo;
    [SerializeField] GameObject mapGo;
    protected GameObject mapGoAttack;
    [SerializeField] Sprite attackWarningSprite;
    protected Bounds mapBounds;
    protected Vector2 playerPos;

    protected Vector2 position; // attack position
    protected float chargeTime = 2f; // time before activate dmg
    protected float radius = 5f; // how close to player can attack
    protected float attackRadius = 3f; // area where can deal damage
    protected SpriteRenderer spriteRenderer;


    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        mapGoAttack = mapGo;
        playerGo = FindObjectOfType<CharacterController>().gameObject;
        mapBounds = mapGo.GetComponent<SpriteRenderer>().bounds;
        playerPos = playerGo.transform.position;

        // set position
        //transform.position = CalculatePosition();

        StartAttacking();

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.GetComponent<CharacterController>().isShielded) return;
            print("Damage");
        }
    }
    protected virtual Vector2 CalculatePosition()
    {
        // attack in some range near player
        position = playerPos + new Vector2(
            Random.Range(-radius, radius),
            Random.Range(-radius, radius)
        );

        // stay in circle radius, not square
        position = (position - playerPos).normalized * Random.Range(0f, radius);
        position = playerPos + position;

        // dont attack outside map
        position.x = Mathf.Clamp(position.x, mapBounds.min.x, mapBounds.max.x);
        position.y = Mathf.Clamp(position.y, mapBounds.min.y, mapBounds.max.y);

        return position;
    }

    protected virtual async void StartAttacking()
    {
        await ChargeAttack();

        await DropDownAttack();

        DealDamage();

        await WaitForTime(0.5f);

        Destroy(gameObject);
    }

    protected virtual async Task ChargeAttack()
    {
        //spriteRenderer.sprite = attackWarningSprite;
        transform.localScale = new Vector2(attackRadius, attackRadius);
        float t = 0f;
        Material material = GetComponent<SpriteRenderer>().material;

        while (t < chargeTime)
        {
            material.SetFloat("_Fill", t);
            t += Time.deltaTime;
            await Task.Yield();
        }
    }

    protected virtual async Task DropDownAttack()
    {
        GameObject attackObjet = transform.GetChild(0).gameObject;
        attackObjet.transform.localScale = new Vector2(1f, 1f);
        Vector2 startPos = attackObjet.transform.position;
        Vector2 endPos = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            attackObjet.transform.position = Vector2.Lerp(startPos, endPos, t);
            await Task.Yield();
        }
    }

    protected virtual void DealDamage()
    {
        gameObject.AddComponent<CircleCollider2D>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        circleCollider.isTrigger = true;
        circleCollider.radius = 0.5f;
        circleCollider.enabled = true;
    }

    protected virtual async Task WaitForTime(float time)
    {
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            await Task.Yield();
        }
    }
}
