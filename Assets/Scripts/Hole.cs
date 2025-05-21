using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private Material material;
    float t;
    float startTime;
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        t = -5f;
    }


    private void Update()
    {
        material.SetFloat("_Fill", t);
        t += Time.deltaTime;

        if (t > 1f)
            Destroy(gameObject);
    }
    /*
    private async Task StartFading()
    {
        float t = 0;
        while(t < 5f)
        {
            t += Time.deltaTime;
            await Task.Yield();
        }
        t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime;
            material.SetFloat("_Fill", t);
            await Task.Yield();
        }

        Destroy(gameObject);

    }
    */
}
