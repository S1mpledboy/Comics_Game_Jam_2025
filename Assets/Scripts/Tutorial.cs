using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hintText;
    CharacterController characterController;
    bool findTreasure = false;
    private void Start()
    {
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
    }

    private async Task StartTutorial()
    {
        bool nextStep = false;
        while (!nextStep)
        {
            if(characterController._helperSignsAmount > 0)
                nextStep = true;
            await Task.Yield();
        }
        hintText.text = "Press F to find way to toy";
        nextStep = false;

        while (!nextStep)
        {
            if (Input.GetKeyDown(KeyCode.F))
                nextStep = true;
            await Task.Yield();
        }
        hintText.text = "Follow sign";
        nextStep= false;

        while (!findTreasure)
        {
            await Task.Yield();
        }
        hintText.text = "Press E to dig";
        nextStep = false;

        while (!nextStep)
        {
            if(characterController.collectedItems > 0)
                nextStep= true;
            await Task.Yield();
        }
        hintText.text = "";

    }
}
