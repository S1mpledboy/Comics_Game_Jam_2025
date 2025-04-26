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
    public static bool digging = false; // is this item digging

    private Material material;

    private void Awake()
    {
        // set random sprite
        randomSprite = Random.Range(0, toysSprites.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = toysSprites[randomSprite];
        material = transform.GetChild(1).GetComponent<SpriteRenderer>().material;
        material.SetFloat("_Fill", 0f);
       
    }
    private void OnDisable()
    {
        Bounds boardBounds = boardGo.GetComponent<SpriteRenderer>().bounds;
        
        Vector2 pos = Vector2.zero;
        pos.x = Random.Range(boardBounds.min.x, boardBounds.max.x);
        pos.y = Random.Range(boardBounds.max.y, boardBounds.min.y);
        gameObject.SetActive(true);
        Instantiate(gameObject, pos, Quaternion.identity);
    }

    // TODO:    na wejœciu pokazuje pasek progresu
    //          na wyjœciu ukrywa pasek progresu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            digging = true;

            collision.gameObject.GetComponent<CharacterController>().toyMaterial = material;
            collision.gameObject.GetComponent<CharacterController>().digTime = digTime;
            collision.gameObject.GetComponent<CharacterController>().toyDigSide = gameObject;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            digging = false;
            
        }
    }



}
