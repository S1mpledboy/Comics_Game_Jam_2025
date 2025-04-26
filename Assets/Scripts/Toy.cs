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
    private bool digging = false; // is this item digging

    private void Awake()
    {
        // set random sprite
        randomSprite = Random.Range(0, toysSprites.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = toysSprites[randomSprite];
    }


    // TODO:    na wejœciu pokazuje pasek progresu
    //          na wyjœciu ukrywa pasek progresu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            digging = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            digging = false;
            
        }
    }

    private void Update()
    {
        if (digging)
        {
            DigToy();
        }
    }

    public void DigToy()
    {
        diggingTime += Time.deltaTime;
        CharacterController.SetAnimation(CharacterController.PlayerStates.Diging);
        if (diggingTime >= digTime)
        {
           
            Bounds boardBounds = boardGo.GetComponent<SpriteRenderer>().bounds;

            Vector2 pos = Vector2.zero;
            pos.x = Random.Range(boardBounds.min.x, boardBounds.max.x);
            pos.y = Random.Range(boardBounds.max.y, boardBounds.min.y);
            Instantiate(gameObject, pos, Quaternion.identity);
            Destroy(gameObject);
            CharacterController.SetAnimation(CharacterController.PlayerStates.Diging);
        }
    }
}
