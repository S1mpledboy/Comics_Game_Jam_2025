using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAttacks : MonoBehaviour
{
    [SerializeField] GameObject[] attacksGo; // all type of attacks


    private Bounds boardBounds;
    private int randomAttack; // index of attack
    private float spawnTime = 1f; // time for spawn attack
    private float spawnCooldown;


    private void Start()
    {
        spawnCooldown = spawnTime;
        boardBounds = gameObject.GetComponent<SpriteRenderer>().bounds;
    }

    private void Update()
    {
        spawnCooldown -= Time.deltaTime;

        if (spawnCooldown < 0f)
        {
            spawnCooldown = spawnTime;
            SpawnAttack();
        }
    }

    // draw random number [0, attacksGo - 1]
    private int DrawSignType()
    {
        return Random.Range(0, attacksGo.Length);
    }

    private void SpawnAttack()
    {
        Vector2 pos = Vector2.zero;
        pos.x = Random.Range(boardBounds.min.x, boardBounds.max.x);
        pos.y = Random.Range(boardBounds.max.y, boardBounds.min.y);

        randomAttack = DrawSignType();

        Instantiate(attacksGo[randomAttack], pos, Quaternion.identity);
    }
}
