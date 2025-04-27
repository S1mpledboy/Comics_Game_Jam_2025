using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Attack : MonoBehaviour
{

    protected GameObject playerGo;
    [SerializeField] GameObject mapGo;
    protected GameObject mapGoAttack;
    [SerializeField] Texture2D sprite;
    protected Bounds mapBounds;
    protected Vector2 playerPos;
    [SerializeField] AudioClip dropSound;
    protected Vector2 position; // attack position
    protected float chargeTime = 2f; // time before activate dmg
    protected float radius = 5f; // how close to player can attack
    protected float attackRadius = 3f; // area where can deal damage
    protected SpriteRenderer spriteRenderer;

    protected Material materialDangerZone;
    protected Material materialAttackObject;


    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        mapGoAttack = mapGo;
        playerGo = FindObjectOfType<CharacterController>().gameObject;
        mapBounds = mapGo.GetComponent<SpriteRenderer>().bounds;
        playerPos = playerGo.transform.position;

        materialDangerZone = GetComponent<SpriteRenderer>().material;
        materialAttackObject = transform.GetChild(0).GetComponent<SpriteRenderer>().material;

        materialAttackObject.SetTexture("_Sprite", sprite);

        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

 

        StartAttacking();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 3)
        {
            CharacterController player = collision.gameObject.GetComponent<CharacterController>();
            if (player.isShielded||player.isInvisible) return;
            CharacterController.OnTakeDamage?.Invoke(-1);

        }
    }

    protected virtual Vector2 CalculatePosition()
    {
        // attack in some range near player
        position = playerPos + new Vector2
        (
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

        DealDamage();

        await WaitForTime(0.1f);

        await FadeAway();
    }

    protected virtual async Task ChargeAttack()
    {

        GameObject attackObjet = transform.GetChild(0).gameObject;
        Vector2 startPos = attackObjet.transform.position;
        Vector2 endPos = transform.position;

        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        float t = 0f;

        while (t < 1f)
        {
            materialDangerZone.SetFloat("_Fill", t);
            attackObjet.transform.position = Vector2.Lerp(startPos, endPos, t);
            t += Time.deltaTime;
            await Task.Yield();
        }
    }



    protected virtual void DealDamage()
    {
        gameObject.AddComponent<CircleCollider2D>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        circleCollider.radius = 0.5f;
        AudioSource.PlayClipAtPoint(dropSound,transform.position);
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

    protected virtual async Task FadeAway()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            materialDangerZone.SetFloat("_Dirt", t);
            materialAttackObject.SetFloat("_Dirt", t);

            await Task.Yield();
        }
        Destroy(gameObject);
    }
}
