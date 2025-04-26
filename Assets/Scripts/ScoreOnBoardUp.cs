using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnBoardUp : MonoBehaviour
{
    float speed = 2f;
    void Update()
    {
        transform.position += new Vector3(0, 0.5f*Time.deltaTime, 0);
    }
}
