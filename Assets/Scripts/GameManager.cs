using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text bonesText;

    private int lives = 3;
    private int bones = 0;

    [SerializeField] GameObject WinScreen;
    [SerializeField] GameObject LoseScreen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        print("awake");
    }

    public void DecreaseLives()
    {
        lives--;
    }
    
    public int GetLives()
    {
        return lives;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetGame()
    {
        Destroy(gameObject);
        Destroy(SoundManager.instance.gameObject);
        Destroy(FindAnyObjectByType<IntroScreenController>());

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WinSequence()
    {
        // Pause player input
        FindAnyObjectByType<PlayerController>().GetComponent<PlayerInput>().DeactivateInput();
        // Show win screen
        Instantiate(WinScreen);
        // Reset the game
        Invoke("ResetGame", 5f);
    }

    public void LoseSequence()
    {
        // Pause player input
        FindAnyObjectByType<PlayerController>().GetComponent<PlayerInput>().DeactivateInput();
        // Show win screen
        Instantiate(LoseScreen);
        // Reset the game
        Invoke("ResetGame", 5f);
    }

}
