using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    [SerializeField] GameObject boardGo;
    private Sprite[] toysSprites;
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
            boardGo.GetComponent<SpawnToys>().SpawnToy();
            Destroy(gameObject);
        }
    }
}
