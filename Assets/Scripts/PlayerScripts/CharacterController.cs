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
    [SerializeField] GameObject _helperSignPrefab, _holePrefab;
    public bool isShielded = false, isSlowed = false, isSpeedBoosted;
    public SpriteRenderer shield;
    [SerializeField] List<GameObject> herarts = new List<GameObject>();
    [SerializeField] List<SpriteRenderer> heartsSp = new List<SpriteRenderer>();
    Dictionary<string, SpriteRenderer> heartsSpritesDic = new Dictionary<string, SpriteRenderer>();
    private bool _isRolling = false;
    public int collectedItems = 0;
    public static float score;
    private int health = 5;
    public float _helperSignsAmount = 3f;
    private float timeToEndGame = 0;

    [SerializeField] Canvas gameplayCanvas;
    [SerializeField] public TextMeshProUGUI _scoreText;
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
            herarts.Reverse();
            foreach (GameObject heart in herarts)
            {
                if(heart.GetComponent<Image>().sprite == heartsSpritesDic["Damage"].sprite)
                {
                    continue;

                }else if (heart.GetComponent<Image>().sprite == heartsSpritesDic["Heal"].sprite)
                {
                    heart.GetComponent<Image>().sprite = heartsSpritesDic["Damage"].sprite;
                    break;
                    
                }
            }

            herarts.Reverse();
        }
        else if (valueOfChange>0)
        {
            foreach (GameObject heart in herarts)
            {
                if (heart.GetComponent<Image>().sprite == heartsSpritesDic["Damage"].sprite)
                {
                    heart.GetComponent<Image>().sprite = heartsSpritesDic["Heal"].sprite;
                    break;
                }
                else if (heart.GetComponent<Image>().sprite == heartsSpritesDic["Heal"].sprite)
                {
                    continue;

                }
            }

        }
        
        if (health <= 0) 
        {

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
        heartsSpritesDic.Add("Heal", heartsSp[0]);
        heartsSpritesDic.Add("Damage", heartsSp[1]);
        heartsSpritesDic.Add("Shield", heartsSp[2]);
        //gameOverCanvas


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
        
        timeToEndGame += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeToEndGame) / 60;
        int seconds = Mathf.FloorToInt(timeToEndGame) % 60;
        _timerText.text = minutes + ":" + seconds;
        _scoreText.text = score.ToString();

        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        if (Input.GetMouseButton(0)) 
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
            
            toyMaterialDirt.SetFloat("_Fill", elapsedTimeOfDigging / digTime);
            elapsedTimeOfDigging += Time.deltaTime;

            if (elapsedTimeOfDigging >= digTime)
            {
                elapsedTimeOfDigging = 0;
                toyDigSide.gameObject.SetActive(false);
                SetAnimation(PlayerStates.Idle);
                collectedItems++;
                _itemsCountText.text = collectedItems.ToString();
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
        if (Input.GetMouseButtonDown(1) )
        {
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


    public void Reset()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
