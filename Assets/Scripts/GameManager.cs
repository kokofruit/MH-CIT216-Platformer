using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int lives = 3;
    private int bones = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        bones = 0;
    }

    public void DecreaseLives()
    {
        lives--;
        if (lives == 0)
        {
            print("die");
        }
    }

    public int GetLives()
    {
        return lives;
    }

    public void IncreaseBones()
    {
        bones++;
    }
}
