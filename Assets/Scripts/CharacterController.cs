using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    float _horizontalMovement, _verticalMovement;
    float _maxSpeed;
    public float  currentspeed;
    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public void SetSpeed(float newSpeed)
    {
        print("speedchange");
        if (newSpeed <= 0)
        {
            currentspeed = 2f;
        }else if (newSpeed > _maxSpeed)
        {
            currentspeed = _maxSpeed;
        }else
        {
            currentspeed = newSpeed;
        }
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
