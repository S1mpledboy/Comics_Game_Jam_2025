using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LocatedHelperSign : MonoBehaviour
{

    private void OnEnable()
    {
        Transform toy = GameObject.FindGameObjectWithTag("Toy").transform;
        Vector3 look = transform.InverseTransformPoint(toy.transform.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
        transform.Rotate(0, 0, angle);
    }
 
}
