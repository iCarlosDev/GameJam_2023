using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerStorage _playerStorage;

    [Header("--- MOVEMENT PARAMETERS ---")]
    [Space(10)]
    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;
    [SerializeField] private float speed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float turnSmoothTime;
    
    [Header("--- GRAVITY PARAMETERS ---")]
    [Space(10)]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float gravity;
    [SerializeField] private float sphereRadius;
    [SerializeField] private bool isGrounded;

    [Header("--- DASH PARAMETERS ---")] 
    [Space(10)]
    [SerializeField] private ParticleSystem swordParticles;
    [SerializeField] private float dashMultiplicator;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashTimeCooldown;
    [SerializeField] private bool dashCombo;
    private Coroutine movementCooldown;
    private Coroutine dashCooldown;

    [Header("--- FOOTSTEPS SOUNDS ---")] 
    [Space(10)] 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int footStepsSoundsIndex;
    [SerializeField] private List<AudioClip> footStepsSoundsList;
    
    private Coroutine finishCombo;

    //GETTERS && SETTERS//
    public Animator Animator => _animator;
    public Coroutine DashCooldown
    {
        get => dashCooldown;
        set => dashCooldown = value;
    }
    public Coroutine MovementCooldown
    {
        get => movementCooldown;
        set => movementCooldown = value;
    }
    public bool DashCombo
    {
        get => dashCombo;
        set => dashCombo = value;
    }
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    public float CurrentSpeed
    {
        get => currentSpeed;
        set => currentSpeed = value;
    }
    public float TurnSmoothTime
    {
        get => turnSmoothTime;
        set => turnSmoothTime = value;
    }

    ///////////////////////////////////////

    private void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
        _playerStorage = GetComponent<PlayerStorage>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        swordParticles.Stop();

        currentSpeed = speed;
    }

    private void Update()
    {
        Movement();

        CalculateGravity();

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Fire1")) && speed > 2)
        {
            Dash();
        }
    }

    private void Movement()
    {
        //Guardo en estas variables las teclas WASD;
        if (movementCooldown == null)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        //Recogemos los valores WASD en positivo y los guardo como "direction";
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        //Si el jugador está en movimiento...;
        if(direction.magnitude >= 0.1f)
        {
            //Recogemos el ángulo del input entre la dirección "X" y "Z" y la proyección del angulo entre esos 2 valores lo convertimos en grados para saber hacia donde mira nuestro player;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            //Smoothea la posición actual a la posición obtenida en "targetAngle";
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, turnSmoothTime);
            //rota el personaje en el eje "Y";
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //guardamos donde rota en "Y" el player por su eje "Z" (dando así que siempre donde mire el player será al frente);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //Movemos el player hacia el frente;
            _characterController.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
            
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }
    }
    
    private void CalculateGravity()
    {
        //Seteamos el bool con una esfera invisible triggered;
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        //si estamos en el suelo y no estamos cayendo el eje "Y" será "-2";
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //El eje "Y" irá progresivamente a 0;
        velocity.y += gravity * Time.deltaTime;
        
        //Movemos al personaje en el eje "Y" cada vez que saltemos;
        _characterController.Move(velocity * Time.deltaTime);
    }

    //Método para Dashear
    private void Dash()
    {
        //Hasta que la corrutina del dash no haya acabado no podrás volver a dashear (Hace que no puedas dashear infinitamente);
        if (movementCooldown != null || dashCooldown != null)
        {
            return; 
        }
        
        //Empezamos corrutina para no poder movernos mientras dasheamos;
        movementCooldown = StartCoroutine(MovementCooldown_Coroutine());
        
        //Cambiamos las wights de la layer de la pose "EnGuardia" y la layer "Ataque" por un problema de animación;
        _animator.SetLayerWeight(1, 0);
        _animator.SetLayerWeight(2, 1);
        //Activamos los triggers para hacer las animaciones "Dash" y "Attack";
        _animator.SetTrigger("Dash");
        _animator.SetTrigger("Attack");
        _animator.SetBool("IsDashing", true);
        
        AudioManager.instance.PlayOneShot("SwordSwing");
        swordParticles.Play();
    }
    
    //Corrutina para no poder movernos mientras dasheamos;
    private IEnumerator MovementCooldown_Coroutine()
    {
        dashCombo = false;
        
        //Multiplicamos la velocidad para llegar más lejos;
        currentSpeed *= dashMultiplicator;
        
        //Reseteamos los ejes horizontal y vertical de las teclas WASD para recorrer la misma distancia quieto o en movimiento;
        horizontal = 0f;
        vertical = 0f;
        
        float contador = 0f;
        //Mientras el contador sea menor al tiempo del dash seguiremos dasheando;
        while (contador < dashTime)
        {
            _characterController.Move(transform.forward * currentSpeed * Time.deltaTime);
            contador += Time.deltaTime;
            yield return null;
        }

        if (movementCooldown != null)
        {
            StopDashing();
        }
    }
    
    public void StopDashing()
    {
        //Volvemos a cambiar los weights de las layers para tenerlo por defecto;
        _animator.SetLayerWeight(1, 1);
        _animator.SetLayerWeight(2, 0);
        
        _animator.SetBool("IsDashing", false);

        currentSpeed = speed;
        
        movementCooldown = null;

        if (!dashCombo)
        {
            //Empezamos corrutina para tener un cooldown para poder dashear otra vez;
            dashCooldown = StartCoroutine(DashCooldown_Coroutine());
        }
    }

    //Corrutina para tener un cooldown para poder dashear otra vez;
    private IEnumerator DashCooldown_Coroutine()
    {
        yield return new WaitForSeconds(dashTimeCooldown);
        dashCooldown = null;
    }

    public void FinishCombo()
    {
        if (finishCombo != null)
        {
            StopCoroutine(finishCombo);
            finishCombo = null;
        }
        
        finishCombo = StartCoroutine(FinishCombo_Coroutine());
    }
    
    private IEnumerator FinishCombo_Coroutine()
    {
        yield return new WaitForSeconds(_playerStorage.PlayerWeapon.TimeToFinishCombo);
        _playerStorage.PlayerWeapon.ComboNumber = 0;
        
        //Dejamos las speed por defecto;
        speed = 5;
        currentSpeed = speed;
        _playerStorage.Trail.time = 0.1f;
    }

    public void FootStepsSoundsController()
    {
        _audioSource.clip = footStepsSoundsList[footStepsSoundsIndex];
        //_audioSource.PlayOneShot(_audioSource.clip);
        _audioSource.Play();
        
        footStepsSoundsIndex++;

        if (footStepsSoundsIndex > 2)
        {
            footStepsSoundsIndex = 0;
        }
    }
}
