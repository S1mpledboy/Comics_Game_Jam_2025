using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    float _horizontalMovement, _verticalMovement;
    public float  currentspeed;
    Rigidbody2D rigidbody;
    static Animator _animator;
    public enum PlayerStates
    {
        Idle,
        Move,
        Diging
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        SetAnimation(PlayerStates.Idle);
    }
 
    // Update is called once per frame
    void Update()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        
    }
    private void FixedUpdate()
    {
        if (_horizontalMovement == 0 && _verticalMovement == 0) 
        {
            SetAnimation(PlayerStates.Idle);
            return;
        }
        SetAnimation(PlayerStates.Move);
        Vector3 directon = new Vector3(_horizontalMovement, _verticalMovement).normalized;
        rigidbody.MovePosition(transform.position+directon*(currentspeed*Time.deltaTime));
    }

    public static void SetAnimation(PlayerStates state)
    {
        switch (state)
        {
            case PlayerStates.Idle:
                _animator.SetBool("IsIdle", true);
                _animator.SetBool("IsMoving", false);
                _animator.SetBool("IsDigging", false);
                break;
            case PlayerStates.Move:
                _animator.SetBool("IsIdle", false);
                _animator.SetBool("IsMoving", true);
                _animator.SetBool("IsDigging", false);
                break;
            case PlayerStates.Diging:
                _animator.SetBool("IsIdle", false);
                _animator.SetBool("IsMoving", false);
                _animator.SetBool("IsDigging", true);
                break;
        }

    }

}
