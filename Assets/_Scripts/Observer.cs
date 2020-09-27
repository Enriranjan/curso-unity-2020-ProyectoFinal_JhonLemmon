using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Observer : MonoBehaviour
{
    private Transform player;

    private bool isPlayerInRange;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            Vector3 direction = player.position - this.transform.position + Vector3.up;
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


}
