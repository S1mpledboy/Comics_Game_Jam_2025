using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
using JetBrains.Annotations;
using System.Threading.Tasks;

public class CharacterController : MonoBehaviour
{
    public static Action<int> OnTakeDamage;
    public static float score;
    static Animator _animator;
    public static float timeToEndGame = 0;

    public GameObject toyDigSide;
    public Material toyMaterialBar, toyMaterialDirt;
    public float  currentspeed, digTime;
    public bool isShielded = false, isInvisible = false;
    public SpriteRenderer shield;
    public int collectedItems = 0;
    public float _helperSignsAmount = 3f, invisibilityTime = 2f;
    public AudioSource playerSFX;
    public AudioClip walkSFX, healSFX, shieldSFX, dodgeSFX, damageSFX, diggingSFX;
    public TextMeshProUGUI _scoreText;

    private bool canRoll = true;
    private float speed = 1f;
    private float rollTime, elapsedTimeOfDigging;
    Rigidbody2D rigidbody;
    private bool _isRolling = false;
    private int health = 4;
    float _horizontalMovement, _verticalMovement;
    int currentHeart = 0;
    private float diggingCooldown = 1f;
    private bool canDigging = true;
    Vector2 digPos;
    private Queue<GameObject> helperSigns = new Queue<GameObject>();
    private GameObject locatedSign;

    [SerializeField] GameObject _helperSignPrefab, _holePrefab;

    [SerializeField] List<GameObject> herarts = new List<GameObject>();
    [SerializeField] List<SpriteRenderer> heartsSp = new List<SpriteRenderer>();
    Dictionary<string, SpriteRenderer> heartsSpritesDic = new Dictionary<string, SpriteRenderer>();
    [SerializeField] Canvas gameplayCanvas;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] TextMeshProUGUI _itemsCountText;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] TextMeshProUGUI _itemsCountGameOverText;
    [SerializeField] TextMeshProUGUI _timerGameOverText;
    [SerializeField] GameObject diggingPlace;
    [SerializeField] Image diggingCooldownBar;


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
        if (health > 4) 
        {
            health = 4;
        }
   
        
        if (valueOfChange < 0 && currentHeart<4)
        {
            AudioSource.PlayClipAtPoint(damageSFX, transform.position);
            herarts[currentHeart].GetComponent<Image>().sprite = heartsSpritesDic["Damage"].sprite;
            currentHeart++;
        }
        else if (valueOfChange>0 && currentHeart-1<4)
        {
            if (currentHeart - 1 < 0)
            {
                currentspeed = 1;
            }
            AudioSource.PlayClipAtPoint(healSFX, transform.position);
            herarts[currentHeart-1].GetComponent<Image>().sprite = heartsSpritesDic["Heal"].sprite;
            currentHeart--;

        }
        
        if (health <= 0) 
        {
            gameOverCanvas.gameObject.SetActive(true);
            gameplayCanvas.gameObject.SetActive(false);
            
        }

    }

    private void Awake()
    {
        OnTakeDamage += TakeDamage;
        OnTakeDamage += SetInvisibility;
        PopulateArrowPool();
    }
    void PopulateArrowPool()
    {
        helperSigns.Enqueue(Instantiate(_helperSignPrefab, new Vector2(-300f, 0f), Quaternion.identity));
        helperSigns.Enqueue(Instantiate(_helperSignPrefab, new Vector2(-300f, 0f), Quaternion.identity));
        helperSigns.Enqueue(Instantiate(_helperSignPrefab, new Vector2(-300f, 0f), Quaternion.identity));
    }
    void SetInvisibility(int HPchange)
    {
        if (HPchange > 0) return; 
        isInvisible = true;
        Color tempAlpha = gameObject.GetComponent<SpriteRenderer>().color;
        tempAlpha.a = 0.4f;
        gameObject.GetComponent<SpriteRenderer>().color = tempAlpha;
        Invoke(nameof(DisableInvisibility), invisibilityTime);
    }
    void DisableInvisibility()
    {
        Color tempAlpha = gameObject.GetComponent<SpriteRenderer>().color;
        tempAlpha.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = tempAlpha;
        isInvisible = false;
    }
    private void OnDestroy()
    {
        OnTakeDamage -= TakeDamage;
        OnTakeDamage -= SetInvisibility;
    }
    private void OnDisable()
    {
        OnTakeDamage -= TakeDamage;
        OnTakeDamage -= SetInvisibility;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        shield.gameObject.SetActive(false);
        playerSFX = GetComponent<AudioSource>();
        
        score = 0f;
        timeToEndGame = 0;
        

        heartsSpritesDic.Add("Heal", heartsSp[0]);
        heartsSpritesDic.Add("Damage", heartsSp[1]);
        heartsSpritesDic.Add("Shield", heartsSp[2]);

        SetAnimation(PlayerStates.Idle);
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
        GetMousePosAndDigPosition();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        if (Input.GetMouseButton(0) && canDigging) 
        {
            Dig();
        }
        // roll
        if (Input.GetKeyDown(KeyCode.Space) && canRoll)
        {
            Dodge();
        }
        PlaceSign();
        CheckSpeed();
    }
    void GetMousePosAndDigPosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Vector2 screenPos = Input.mousePosition;

        digPos = (Vector2)Camera.main.ScreenToWorldPoint(screenPos)
            - (Vector2)transform.position;
        digPos = digPos.normalized * 1.6f;
        digPos = digPos + (Vector2)transform.position + new Vector2(0.5f, 0f);

        diggingPlace.transform.position = digPos;
    }
    void Dig()
    {
        TryDig();
        _horizontalMovement = _verticalMovement = 0;
        if (!Toy.digging)
            DiggingStartCooldown();
    }
    void CountTime()
    {
        timeToEndGame += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeToEndGame) / 60;
        int seconds = Mathf.FloorToInt(timeToEndGame) % 60;
        _timerText.text = minutes + ":" + seconds;
        _scoreText.text = score.ToString();
    }
    void Dodge()
    {
        AudioSource.PlayClipAtPoint(dodgeSFX, transform.position);
        SetAnimation(PlayerStates.Doging);
        canRoll = false;
        _isRolling = true;
        speed = 1.7f;
        rollTime = 0.25f;
    }
    void RestoreSpeed()
    {
        currentspeed = 7;
    }
    private async Task DiggingStartCooldown()
    {
        canDigging = false;
        float t = 0f;
        while (t < diggingCooldown)
        {
            t+= Time.deltaTime;
            diggingCooldownBar.fillAmount = t / diggingCooldown;

            await Task.Yield();
        }
        canDigging = true;
    }
    void TryDig()
    {
       SetAnimation(PlayerStates.Diging);
        
        playerSFX.clip = diggingSFX;
        playerSFX.Play();
        if (Toy.digging)
        {
            
            toyMaterialDirt.SetFloat("_Fill", elapsedTimeOfDigging / digTime);
            elapsedTimeOfDigging += Time.deltaTime;

            if (elapsedTimeOfDigging >= digTime)
            {
                elapsedTimeOfDigging = 0;
                toyDigSide.gameObject.SetActive(false);
                playerSFX.Stop();
                SetAnimation(PlayerStates.Idle);

                collectedItems++;
                _itemsCountText.text = collectedItems.ToString();

            } 
        }
        else
        {
            GameObject hole = Instantiate(_holePrefab, digPos, transform.rotation);
            Material holeMaterial = hole.GetComponent<SpriteRenderer>().material;
            holeMaterial.SetFloat("_Fill", 0f);
            playerSFX.Stop();

        }
    }
    void CheckSpeed()
    {
        if (currentspeed <= 2)
        {
            currentspeed = 2f;
            Invoke(nameof(RestoreSpeed), 2f);
        }
    }
    void PlaceSign()
    {
        if (Input.GetMouseButtonDown(1) )
        {
            //GameObject locatedSign = Instantiate(_helperSignPrefab, transform.position, Quaternion.identity);
            locatedSign = helperSigns.Dequeue();
            locatedSign.GetComponent<LocatedHelperSign>().Init(transform.position);
            //locatedSign.transform.position = transform.position;
            helperSigns.Enqueue(locatedSign);
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
            
            playerSFX.clip = walkSFX;
            playerSFX.Play();
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
        score = 0;
        timeToEndGame = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
