using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private Material material;
    float t;
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        StartFading();

    }
  

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
}
