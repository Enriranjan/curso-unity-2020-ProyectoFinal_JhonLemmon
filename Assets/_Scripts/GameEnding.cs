using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField] public float fadeDuration = 1f;
    private float timer = 0;

    private GameObject player;

    public bool isPlayerAtExit, isPlayerDead;

    public CanvasGroup exitBackgroundImageCanvasGroup, deadBackgroundImageCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false);
        }
        else if (isPlayerDead)
        {
            EndLevel(deadBackgroundImageCanvasGroup, true);

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
    /// Se encargara de desvanecer la imagen de Game Over, y de cerrar o resetear el juego
    /// </summary>
    /// <param name="imageCanvasGroup">El canvas que va apareciendo</param>
    /// <param name="isRestart">Si deseamos reiniciar la partida</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool isRestart)
    {
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration)
        {
            if (isRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Application.Quit();
            }
            
        }
    }

    public void CaughtPlayer()
    {
        isPlayerDead = true;
    }
}
