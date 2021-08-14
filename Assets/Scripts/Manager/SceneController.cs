using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; } = null;

    private bool hidden;

    public bool Hidden
    {
        get { return hidden; }
        set 
        { 
            hidden = value;
            SceneManager.LoadScene("EndingScene");
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
}
