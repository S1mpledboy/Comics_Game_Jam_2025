using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSigns : MonoBehaviour
{
    [SerializeField] GameObject[] signsGo; // toy graphic


    private Bounds boardBounds;
    private int randomSign; // index of signsGo
    private float spawnTime = 1f; // time for spawn sign
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
            SpawnSign();
        }
    }

    // draw random number [0, singsGo - 1]
    private int DrawSignType()
    {
        return Random.Range(0, signsGo.Length);
    }

    private void SpawnSign()
    {
        Vector2 pos = Vector2.zero;
        pos.x = Random.Range(boardBounds.min.x, boardBounds.max.x);
        pos.y = Random.Range(boardBounds.max.y, boardBounds.min.y);

        randomSign = DrawSignType();

        Instantiate(signsGo[randomSign], pos, Quaternion.identity);
    }
}
