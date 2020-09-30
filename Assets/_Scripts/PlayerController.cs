#if UNITY_IOS || UNITY_ANDROID
    #define USING_MOBILE
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    //MOVEMENT OF PLAYER
    [SerializeField] private float turnSpeed;

    private Vector3 movement;
    private Quaternion rotation = Quaternion.identity;

    private Rigidbody _rigidbody;
    
    //ANIMATION OF PLAYER
    private Animator _animator;
    private const string IS_WALKING = "IsWalking";
    
    //GAME LOGIC
    private GameEnding gameEnding;
    
    //AUDIO
    [SerializeField] private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        
        _animator = this.GetComponent<Animator>();

        _audioSource = this.GetComponent<AudioSource>();

        gameEnding = GameObject.FindWithTag("Game Ending").GetComponent<GameEnding>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //capturamos los Input horizontal y vertical
        #if USING_MOBILE
            float horizontalInput = Input.GetAxis("Mouse X");
            float verticalInput = Input.GetAxis("Mouse Y");
            if(Input.touchCount > 0)
            {
                horizontalInput = Input.touches[0].deltaPosition.x;
                verticalInput = Input.touches[0].deltaPosition.y;
            }
        #else
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
        #endif
        
      
        
        //introducimos el Input en un Vector3
        movement.Set(horizontalInput, 0, verticalInput);
        movement.Normalize();

        //comprobamos si el personaje se mueve en algun eje para ejecutar la animacion de andar
        bool hasHorizontalInput = !Mathf.Approximately(horizontalInput, 0f);
        bool hasVerticalInput = !Mathf.Approximately(verticalInput, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        
        _animator.SetBool(IS_WALKING, isWalking);

        if (isWalking)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            
        }
        else
        {
            _audioSource.Stop();
        }

        /*calculamos hacia donde queremos que mire nuestro personaje a partir de su rotacion actual,
         la rotacion que aplicamos con el Input, la velocidad de giro, y*/
        Vector3 desiredForward = Vector3.RotateTowards(this.transform.forward, movement, 
            turnSpeed * Time.fixedDeltaTime, 0f);
        
        //convertimos ese Vector3 a un Quaternion mediante LookRotation
        rotation = Quaternion.LookRotation(desiredForward);
        

    }

    private void OnAnimatorMove()
    {
        //S = S0 + V * t
        
        //aplicamos el movimiento usando la posicion actual del rb + hacia donde se mueve + delta de la animacion
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        
        //aplicamos la rotacion en forma de Quaternion calculada anteriormente
        _rigidbody.MoveRotation(rotation);
    }
}
