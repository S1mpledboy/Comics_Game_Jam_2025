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

        while (t < 1f)
        {
            material.SetFloat("_Fill", t);
        }

        Destroy(gameObject);

    }
}
