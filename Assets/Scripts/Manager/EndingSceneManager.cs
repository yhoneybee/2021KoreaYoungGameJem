using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour
{
    public Image img;

    public List<Sprite> Hidden = new List<Sprite>();
    public List<Sprite> Normal = new List<Sprite>();

    int index = 0;

    private void Start()
    {
        if (SceneController.Instance.Hidden)
        {
            SoundManager.Instance.Add("e_backTracking_backTracking_1");
            SoundManager.Instance.Add("e_backTrackingEnd_backTrackingEnd_1");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SceneController.Instance.Hidden)
            {
                img.sprite = Hidden[index];
                ++index;
                if (index == Hidden.Count)
                {
                    SceneManager.LoadScene("Ingame");
                }
            }
            else
            {
                img.sprite = Normal[index];
                ++index;
                if (index == Normal.Count)
                {
                    SceneManager.LoadScene("Ingame");
                }
            }

        }
    }
}
