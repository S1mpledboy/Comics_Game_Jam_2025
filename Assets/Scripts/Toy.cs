using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toy : MonoBehaviour
{
    [SerializeField] GameObject boardGo, scoreOb;
    [SerializeField] Sprite[] toysSprites;
    private int randomSprite;

    private float digTime = 2f; // time to get item
    private float diggingTime; // how long this toy was digging
    public static bool digging = false; // is this item digging

    private Material materialBar;
    private Material materialDirt;

    [SerializeField] float hiddenLevel;

    private void Awake()
    {
        // set random sprite
        randomSprite = Random.Range(0, toysSprites.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = toysSprites[randomSprite];
        materialBar = transform.GetChild(1).GetComponent<SpriteRenderer>().material;
        materialBar.SetFloat("_Fill", 0f);
        materialDirt = transform.GetChild(2).GetComponent<SpriteRenderer>().material;
        materialDirt.SetFloat("_Fill", 0f);

        materialDirt.SetFloat("_HiddenLevel", hiddenLevel);
    }


    // TODO:    na wejœciu pokazuje pasek progresu
    //          na wyjœciu ukrywa pasek progresu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DigginPlace"))
        {
            print("moze kopac");
            digging = true;
            collision.gameObject.transform.parent.GetComponent<CharacterController>().toyDigSide = gameObject;
            collision.gameObject.transform.parent.GetComponent<CharacterController>().digTime = digTime;
            collision.gameObject.transform.parent.GetComponent<CharacterController>().toyMaterialBar = materialBar;
            collision.gameObject.transform.parent.GetComponent<CharacterController>().toyMaterialDirt = materialDirt;


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Wychodzi");
        if (collision.gameObject.CompareTag("DigginPlace"))
        {
            digging = false;
            
        }
    }

  
    private void OnDisable()
    {
        Bounds boardBounds = boardGo.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds;

        Vector2 pos = Vector2.zero;
        pos.x = Random.Range(boardBounds.min.x, boardBounds.max.x);
        pos.y = Random.Range(boardBounds.max.y, boardBounds.min.y);
        GameObject scoreNumber = Instantiate(scoreOb, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        scoreNumber.transform .GetChild(0).GetComponent<TextMeshProUGUI>().text = 50f.ToString();
        CharacterController.score += 50f;
        Destroy(scoreNumber, 1f);
        gameObject.SetActive(true);
        Instantiate(gameObject, pos, Quaternion.identity);
    }

}
