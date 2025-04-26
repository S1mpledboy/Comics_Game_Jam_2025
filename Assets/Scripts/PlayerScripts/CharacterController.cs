using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
using JetBrains.Annotations;

public class CharacterController : MonoBehaviour
{
    public static Action<int> OnTakeDamage;
    float _horizontalMovement, _verticalMovement;
    public float  currentspeed, digTime;
    private bool canRoll = true;
    private float speed = 1f;
    private float rollTime, elapsedTimeOfDigging; // how long can roll
    Rigidbody2D rigidbody;
    public GameObject toyDigSide;
    static Animator _animator;
    public Material toyMaterialBar;
    public Material toyMaterialDirt;
    public int _helperSignsAmount = 0;
    [SerializeField] GameObject _helperSignPrefab, _holePrefab;
    public bool isShielded = false, isSlowed = false, isSpeedBoosted;
    public SpriteRenderer shield;
    [SerializeField] List<GameObject> herarts = new List<GameObject>();
    [SerializeField] List<SpriteRenderer> heartsSp = new List<SpriteRenderer>();
    Dictionary<string, SpriteRenderer> heartsSpritesDic = new Dictionary<string, SpriteRenderer>();
    private bool _isRolling = false;
    public int collectedItems = 0;

    private int health = 5;

    private float timeToEndGame = 2 * 60 + 1;

    [SerializeField] Canvas gameplayCanvas;
    [SerializeField] TextMeshProUGUI _helperSignsText;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] TextMeshProUGUI _itemsCountText;


    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] TextMeshProUGUI _itemsCountGameOverText;
    [SerializeField] TextMeshProUGUI _timerGameOverText;

    int minutes, seconds;
    public enum PlayerStates
    {
        Idle,
        Move,
        Diging,
        Doging
    }
    void TakeDamage(int valueOfChange)
    {
        health+=valueOfChange;
        int index = 0;
        if (health >= 4) 
        {
            health = 4;
            index = health - 1;
        }
        else
        {
            index = health;
        }
        if (valueOfChange < 0)
        {
            herarts[index].gameObject.GetComponent<Image>().sprite = heartsSpritesDic["Damage"].sprite;

        }
        else if (valueOfChange>0)
        {
            herarts[index].gameObject.GetComponent<Image>().sprite = heartsSpritesDic["Heal"].sprite;

        }
        
        if (health <= 0) 
        {
            gameOverCanvas.gameObject.SetActive(true);
            gameplayCanvas.gameObject.SetActive(false);
            Time.timeScale = 0;
        }

    }
    private void Awake()
    {
        OnTakeDamage += TakeDamage;
    }
    private void OnDestroy()
    {
        OnTakeDamage -= TakeDamage;
    }
    private void OnDisable()
    {
        OnTakeDamage -= TakeDamage;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        shield.gameObject.SetActive(false);
        SetAnimation(PlayerStates.Idle);
        UpdateHelpersSign();
        heartsSpritesDic.Add("Heal", heartsSp[0]);
        heartsSpritesDic.Add("Damage", heartsSp[1]);
        heartsSpritesDic.Add("Shield", heartsSp[2]);


        gameOverCanvas.gameObject.SetActive(false);
    }
    public void PlayShieldHeartAnimation(string hearatName)
    {
        foreach(GameObject heart in herarts)
        {
            if(heart.gameObject.GetComponent<Image>().sprite != heartsSpritesDic["Damage"].sprite)
            {
                heart.gameObject.GetComponent<Image>().sprite = heartsSpritesDic[hearatName].sprite;
            }
            else
            {
                continue;
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        timeToEndGame -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeToEndGame) / 60;
        int seconds = Mathf.FloorToInt(timeToEndGame) % 60;
        _timerText.text = minutes + ":" + seconds;
        

        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.E)) 
        {
            TryDig();
            _horizontalMovement = _verticalMovement = 0;
        }
        // roll
        if (Input.GetKeyDown(KeyCode.Space) && canRoll)
        {
            SetAnimation(PlayerStates.Doging);
            canRoll = false;
            _isRolling = true;
            speed = 1.7f;
            rollTime = 0.25f;
        }
        PlaceSign();
        CheckSpeed();
    }
    void TryDig()
    {
       SetAnimation(PlayerStates.Diging);
        if (Toy.digging)
        {
            toyMaterialBar.SetFloat("_Fill", elapsedTimeOfDigging / digTime);
            toyMaterialDirt.SetFloat("_Fill", elapsedTimeOfDigging / digTime);
            elapsedTimeOfDigging += Time.deltaTime;

            if (elapsedTimeOfDigging >= digTime)
            {
                elapsedTimeOfDigging = 0;
                toyDigSide.gameObject.SetActive(false);
                SetAnimation(PlayerStates.Idle);
                collectedItems++;
            } 
        }
        else
        {
            GameObject hole = Instantiate(_holePrefab, transform.position, transform.rotation);
            Material holeMaterial = hole.GetComponent<SpriteRenderer>().material;
            holeMaterial.SetFloat("_Fill", 0f);
            
        }
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
        

        if (!canRoll && rollTime > 0f)
        {
            rollTime -= Time.deltaTime;
            if (rollTime <= 0f)
            {
                speed = 1f;
                canRoll = true;
                rollTime = 0f;
                _isRolling = false;
            }
            Vector3 directon = new Vector3(_horizontalMovement, _verticalMovement).normalized;
            rigidbody.MovePosition(transform.position + directon * (speed * currentspeed * Time.deltaTime));
        }
        if (_horizontalMovement == 0 && _verticalMovement == 0 && !_isRolling) 
        {
            SetAnimation(PlayerStates.Idle);
            return;
        }else if ((_horizontalMovement != 0 || _verticalMovement != 0) && !_isRolling)
        {
            SetAnimation(PlayerStates.Move);
            Vector3 directon = new Vector3(_horizontalMovement, _verticalMovement).normalized;
            rigidbody.MovePosition(transform.position + directon * (speed * currentspeed * Time.deltaTime));
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
                _animator.SetBool("IsDoging", false);
                break;
            case PlayerStates.Move:
                _animator.SetBool("IsIdle", false);
                _animator.SetBool("IsMoving", true);
                _animator.SetBool("IsDigging", false);
                _animator.SetBool("IsDoging", false);
                break;
            case PlayerStates.Diging:
                _animator.SetBool("IsIdle", false);
                _animator.SetBool("IsMoving", false);
                _animator.SetBool("IsDigging", true);
                _animator.SetBool("IsDoging", false);
                break;
            case PlayerStates.Doging:
                _animator.SetBool("IsIdle", false);
                _animator.SetBool("IsMoving", false);
                _animator.SetBool("IsDigging", false);
                _animator.SetBool("IsDoging", true);
                break;
        }

    }
    public void UpdateHelpersSign()
    {
        _helperSignsText.text = _helperSignsAmount.ToString();
    }

    public void Reset()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
