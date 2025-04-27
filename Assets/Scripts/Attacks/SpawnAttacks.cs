using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnAttacks : MonoBehaviour
{
    [SerializeField] GameObject[] attacksGo; // all type of attacks
    [SerializeField] GameObject board;


    private Bounds boardBounds;
    private int randomAttack; // index of attack
    private float spawnTime = 1f; // time for spawn attack
    private float spawnCooldown;
    private float radius = 5f; // how close to player can attack
    private float sequenceSpace = 3f; // space in sequence attacks


    private void Start()
    {
        spawnCooldown = spawnTime;
        boardBounds = board.GetComponent<SpriteRenderer>().bounds;
    }

    private void Update()
    {
        spawnCooldown -= Time.deltaTime;

        if (spawnCooldown < 0f)
        {
            SpawnAttack();
            spawnCooldown = spawnTime;
        }
    }

    // draw random number [0, attacksGo - 1]
    private int DrawSignType()
    {
        return Random.Range(0, attacksGo.Length);
    }

    private async void SpawnAttack()
    {
        Vector2 pos = CalculatePosition();

        randomAttack = DrawSignType();
        int sequenceAttack = Random.Range(0, 2);
        bool canSequenceAttack = sequenceAttack == 0 ? false : true;
        int numberOfAttacks = randomAttack == 0 ? 6 : 3;
        numberOfAttacks = canSequenceAttack ? numberOfAttacks : 1;

        float randomRotation = Random.Range(0f, 90f);
        Vector2 playerPos = FindObjectOfType<CharacterController>().transform.position;
        for (int i = 0; i < numberOfAttacks; i++)
        {
            Vector2 oneAttackPos = pos;
            Quaternion quaternion = Quaternion.identity;
            if(randomAttack == 0 && canSequenceAttack)
            {
                oneAttackPos.x = sequenceSpace * Mathf.Cos(2 * Mathf.PI / numberOfAttacks * i);
                oneAttackPos.y = sequenceSpace * Mathf.Sin(2 * Mathf.PI / numberOfAttacks * i);
                oneAttackPos += playerPos;
            }
            if(randomAttack == 1 && canSequenceAttack)
            {
                quaternion = Quaternion.Euler(0f, 0f, randomRotation);
                Vector2 vector = Vector2.up;
                vector = quaternion * vector;
                oneAttackPos = oneAttackPos + vector * (i-1) * sequenceSpace;
            }

            Instantiate(attacksGo[randomAttack], oneAttackPos, quaternion);
            await WaitTime(0.1f);
        }
    }

    private async Task WaitTime(float time)
    {
        float t = 0;

        while (t < time)
        {
            t += Time.deltaTime;
            await Task.Yield();
        }
    }

    private Vector2 CalculatePosition()
    {
        Vector2 playerPos = FindObjectOfType<CharacterController>().transform.position;
        // attack in some range near player
        Vector2 position = playerPos + new Vector2(
            Random.Range(-radius, radius),
            Random.Range(-radius, radius)
        );

        // stay in circle radius, not square
        position = (position - playerPos).normalized * Random.Range(0f, radius);
        position = playerPos + position;

        // dont attack outside map
        position.x = Mathf.Clamp(position.x, boardBounds.min.x, boardBounds.max.x);
        position.y = Mathf.Clamp(position.y, boardBounds.min.y, boardBounds.max.y);

        return position;
    }
}
