using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera mainCamera;

    [Header("--- MOVEMENT PARAMETERS ---")]
    [Space(10)]
    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;
    [SerializeField] private float speed;
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
    [SerializeField] private float dashTime;
    private Coroutine movementCooldown;

    private void Update()
    {
        Movement();

        CalculateGravity();

        if (Input.GetKeyDown(KeyCode.LeftShift))
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
            _characterController.Move(moveDir.normalized * speed * Time.deltaTime);
            
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
        if (movementCooldown != null)
        {
            return; 
        }

        movementCooldown = StartCoroutine(MovementCooldown_Coroutine());
        
        _animator.SetTrigger("Dash");
    }

    private IEnumerator MovementCooldown_Coroutine()
    {
        speed *= 8f;
        horizontal = 0f;
        vertical = 0f;
        
        float contador = 0f;
        while (contador < dashTime)
        {
            _characterController.Move(transform.forward * speed * Time.deltaTime);
            contador += Time.deltaTime;
            yield return null;
        }
        
        speed = 5f;
        movementCooldown = null;
        yield return null;
    }
}
