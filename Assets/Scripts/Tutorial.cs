using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hintText;
    CharacterController characterController;

    [SerializeField] GameObject attack;
    [SerializeField] GameObject toy;
    bool findTreasure = false;
    bool findSign = false;
    private void Start()
    {
        toy.transform.GetChild(2).GetComponent<SpriteRenderer>().material.SetFloat("_Fill", 0.3f);
        characterController = GetComponent<CharacterController>();
        StartTutorial();
    }
    private void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Toy"))
        {
            findTreasure = true;
        }

        if (collision.gameObject.CompareTag("Sign"))
        {
            findSign = true;
        }

    }

    private async Task StartTutorial()
    {
        bool nextStep = false;
        while (!findSign)
        {
            await Task.Yield();
        }
        hintText.text = "Press RPM to find way to toy";
        nextStep = false;

        while (!nextStep)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
                nextStep = true;
            await Task.Yield();
        }
        hintText.text = "Follow sign";
        nextStep= false;

        while (!findTreasure)
        {
            await Task.Yield();
        }
        hintText.text = "Hold LPM to dig";
        nextStep = false;

        while (!nextStep)
        {
            if(characterController.collectedItems > 0)
                nextStep= true;
            await Task.Yield();
        }
        hintText.text = "Chaos brother is angry!. Space to roll!";
        for (int i = 0; i < 4; i++)
        {
            Vector2 pos = transform.position;
            pos.x = pos.x - Mathf.Pow(-0.5f, (int)(i / 2));
            pos.y = pos.y - Mathf.Pow(-0.5f, (int)(i % 2));
            Instantiate(attack, pos, Quaternion.identity);
        }
        nextStep = false;
        float t = 0f;
        while (!nextStep)
        {
            t += Time.deltaTime;
            if (t > 5f)
                nextStep = true;
            await Task.Yield();
        }
        hintText.text = "There's so many good and bad signs. Learn and collect all hidden toys. Good luck!";
        nextStep = false;
        
        t = 0f;
        while (!nextStep)
        {
            t += Time.deltaTime;
            if (t > 5f)
                nextStep = true;
            await Task.Yield();
        }
            SceneManager.LoadScene(1);

    }

    public void Reset()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
