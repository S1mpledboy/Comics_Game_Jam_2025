using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    [SerializeField] GameObject boardGo;
    [SerializeField] Sprite[] toysSprites;
    private int randomSprite;

    private float digTime = 2f; // time to get item
    private float diggingTime; // how long this toy was digging

    private void Awake()
    {
        // set random sprite
        randomSprite = Random.Range(0, toysSprites.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = toysSprites[randomSprite];
    }


    // TODO:    na wejœciu pokazuje pasek progresu
    //          na wyjœciu ukrywa pasek progresu
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("kolizja");
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Zbieram");
            DigToy();
        }
    }

    public void DigToy()
    {
        diggingTime += Time.deltaTime;
        if (diggingTime >= digTime)
        {
            print("Zebrano zabawkê");
            Bounds boardBounds = boardGo.GetComponent<SpriteRenderer>().bounds;

            Vector2 pos = Vector2.zero;
            pos.x = Random.Range(boardBounds.min.x, boardBounds.max.x);
            pos.y = Random.Range(boardBounds.max.y, boardBounds.min.y);
            print(pos);
            Instantiate(gameObject, pos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
