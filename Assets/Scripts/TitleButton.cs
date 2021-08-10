using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    [SerializeField] private GameObject settingUI; 

    public void GameStart()
    {
        SceneManager.LoadScene("Ingame");
    }

    public void GameExit()
    {

        Application.Quit();
    }

    public void Setting()
    {
        settingUI.SetActive(!settingUI.activeSelf);
    }
}
