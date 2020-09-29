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

    [SerializeField] private AudioSource exitAudio, caughtAudio;
    private bool hasAudioPlayed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (isPlayerDead)
        {
            EndLevel(deadBackgroundImageCanvasGroup, true, caughtAudio);

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
    /// <param name="audioSource">El sonido que deseamos reproducir</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool isRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }
        
        
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration && !audioSource.isPlaying)
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
