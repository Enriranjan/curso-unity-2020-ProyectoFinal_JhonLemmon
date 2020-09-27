using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Observer : MonoBehaviour
{
    private Transform player;

    private bool isPlayerInRange;

    private Vector3 direction;
        
    [SerializeField] private GameEnding gameEnding;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        
        gameEnding = GameObject.FindWithTag("Game Ending").GetComponent<GameEnding>();
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            direction = player.position - this.transform.position + Vector3.up;
            Ray ray = new Ray(this.transform.position, direction);
            
            Debug.DrawRay(this.transform.position, direction, Color.red, Time.deltaTime, true);

            RaycastHit raycastHit;
            
            //si chocamos contra algo dara true
            if (Physics.Raycast(ray, out raycastHit))
            {
                //si chocamos con algo que este en la MIMSA transform que nuestro Player
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, 0.1f);
    }
}
