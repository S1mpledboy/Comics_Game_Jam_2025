using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterController : MonoBehaviour
{
    float _horizontalMovement, _verticalMovement;
    public float  currentspeed;
    private bool canRoll = true;
    private float speed = 1f;
    private float rollTime; // how long can roll
    Rigidbody2D rigidbody;
    static Animator _animator;
    [SerializeField] TextMeshProUGUI _helperSignsText;
    public int _helperSignsAmount = 0;
    [SerializeField] GameObject _helperSignPrefab;
    public bool isShielded = false;
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
        UpdateHelpersSign();

    }
 
    // Update is called once per frame
    void Update()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        PlaceSign();
        CheckSpeed();
        
    }
    void CheckSpeed()
    {
        if (currentspeed < 0)
        {
            currentspeed = 2;
        }
    }
    void PlaceSign()
    {
        if (Input.GetKeyDown(KeyCode.F) && _helperSignsAmount > 0)
        {
            _helperSignsAmount--;
            UpdateHelpersSign();

            GameObject locatedSign = Instantiate(_helperSignPrefab, transform.position, Quaternion.identity);

        }
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
        rigidbody.MovePosition(transform.position+directon*(speed * currentspeed*Time.deltaTime));

        // roll
        if (Input.GetKeyDown(KeyCode.Space) && canRoll)
        {
            print("Roll");
            canRoll = false;
            speed = 1.7f;
            rollTime = 0.2f;
        }

        if(!canRoll && rollTime > 0f)
        {
            rollTime -= Time.deltaTime;
            if(rollTime <= 0f)
            {
                speed = 1f;
                canRoll = true;
                rollTime = 0f;
            }
        }
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
    public void UpdateHelpersSign()
    {
        _helperSignsText.text = _helperSignsAmount.ToString();
    }
}
