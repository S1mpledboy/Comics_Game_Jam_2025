using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    float _horizontalMovement, _verticalMovement;
    public float  currentspeed;
    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
 
    // Update is called once per frame
    void Update()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        
    }
    private void FixedUpdate()
    {
        if (_horizontalMovement == 0 && _verticalMovement == 0) return;
        Vector3 directon = new Vector3(_horizontalMovement, _verticalMovement).normalized;
        rigidbody.MovePosition(transform.position+directon*(currentspeed*Time.deltaTime));
    }
}
