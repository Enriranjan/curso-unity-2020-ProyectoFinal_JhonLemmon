using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    [SerializeField] public float fadeDuration = 1f;
    private float timer = 0;

    private GameObject player;

    public bool isPlayerAtExit;

    public CanvasGroup exitBackgroundImageCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (isPlayerAtExit)
        {
            timer += Time.deltaTime;
            exitBackgroundImageCanvasGroup.alpha = timer / fadeDuration;

            if (timer > fadeDuration)
            {
                EndLevel();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            isPlayerAtExit = true;
        }
    }
    
    /// <summary>
    /// Se encargara de desvanecer la imagen de Game Over, y de cerrar el juego
    /// </summary>
    void EndLevel()
    {
        Application.Quit();
    }
}
