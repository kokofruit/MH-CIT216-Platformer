using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntroScreenController : MonoBehaviour
{
    public static IntroScreenController instance;
    bool isVisible = true;

    private void Awake()
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
    }

    private void Start()
    {
        if (isVisible) FindAnyObjectByType<PlayerInput>().DeactivateInput();
    }

    public void CloseIntro()
    {
        isVisible = false;
        Destroy(transform.GetChild(1).gameObject);
        Destroy(transform.GetChild(0).gameObject);
        FindAnyObjectByType<PlayerInput>().ActivateInput();
    }

}
