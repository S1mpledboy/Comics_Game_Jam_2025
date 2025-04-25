using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnToys : MonoBehaviour
{
    [SerializeField] GameObject board; // game map
    [SerializeField] GameObject toyGo; // toy graphic


    private Bounds boardBounds;

    private void Start()
    {
        boardBounds = gameObject.GetComponent<SpriteRenderer>().bounds;

        // spawn starter toys
        for(int i = 0; i < 3; i++)
        {
            Vector2 pos = Vector2.zero;
            pos.x = Random.Range(boardBounds.min.x, boardBounds.max.x);
            pos.y = Random.Range(boardBounds.max.y, boardBounds.min.y);

            Instantiate(toyGo, pos, Quaternion.identity);

        }
    }
}
