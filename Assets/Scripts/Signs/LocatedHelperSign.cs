using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LocatedHelperSign : MonoBehaviour
{
    [SerializeField] GameObject _poleGo;
    private void OnEnable()
    {
        Init();
       
        
    }

    public void Init()
    {
        Transform toy = GameObject.FindGameObjectWithTag("Toy").transform;
        Vector3 look = transform.InverseTransformPoint(toy.transform.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, angle);
        //Instantiate(_poleGo, transform.position, Quaternion.identity);
        //Invoke(nameof(DisableArrow), 7f);
    }

    void DisableArrow()
    {
        Destroy(gameObject);
    }
 
}
