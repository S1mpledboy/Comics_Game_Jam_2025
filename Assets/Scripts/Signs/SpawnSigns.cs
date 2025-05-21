using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSigns : MonoBehaviour
{
    [SerializeField] GameObject[] signsGo; // toy graphic
    [SerializeField] GameObject board;


    private Bounds boardBounds;
    private int randomSign; // index of signsGo
    private float spawnTime = 5f; // time for spawn sign
    private float spawnCooldown;
    private float _startTime;


    private void Start()
    {
        spawnCooldown = spawnTime;
        boardBounds = board.GetComponent<SpriteRenderer>().bounds;
        _startTime = Time.time;
    }

    private void Update()
    {
        spawnCooldown -= Time.deltaTime;

        if (spawnCooldown < 0f)
        {
            spawnCooldown = 1f;
            //spawnCooldown = Mathf.Lerp(spawnTime, 1f, (Time.time - _startTime) / (60 * 5));
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
