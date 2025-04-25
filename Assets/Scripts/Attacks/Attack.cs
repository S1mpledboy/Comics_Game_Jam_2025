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

    [SerializeField] GameObject playerGo;
    [SerializeField] GameObject mapGo;
    [SerializeField] Sprite attackWarningSprite;
    protected Bounds mapBounds;
    protected Vector2 playerPos;

    protected Vector2 position; // attack position
    protected float chargeTime = 2f; // time before activate dmg
    protected float radius = 5f; // how close to player can attack
    protected SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerGo = FindObjectOfType<CharacterController>().gameObject;
        mapBounds = mapGo.GetComponent<SpriteRenderer>().bounds;
        playerPos = playerGo.transform.position;

        // set position
        transform.position = CalculatePosition();

        StartAttacking();

    }

    private Vector2 CalculatePosition()
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

    private async void StartAttacking()
    {
        await ChargeAttack();

        Destroy(gameObject);
    }

    private async Task ChargeAttack()
    {
        //spriteRenderer.sprite = attackWarningSprite;
        float t = 0f;
        Color color = spriteRenderer.color;

        while (t < chargeTime)
        {
            color.a = t / chargeTime;
            spriteRenderer.color = color;
            t += Time.deltaTime;
            await Task.Yield();
        }
    }
}
