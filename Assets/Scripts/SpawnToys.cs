using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnToys : MonoBehaviour
{
    [SerializeField] GameObject board; // game map
    [SerializeField] GameObject toyGo; // toy graphic


    private Bounds boardBounds;

    private void Awake()
    {
        boardBounds = gameObject.GetComponent<SpriteRenderer>().bounds;      
        for(int i = 0; i < 3; i++)
        {
            SpawnToy();

        }
    }

    public void SpawnToy()
    {
        
        Vector2 pos = Vector2.zero;
        print(boardBounds.min.x + " " + boardBounds.min.y);
        pos.x = Random.Range(boardBounds.min.x, boardBounds.max.x);
        pos.y = Random.Range(boardBounds.max.y, boardBounds.min.y);

        Instantiate(toyGo, pos, Quaternion.identity);
    }
}
