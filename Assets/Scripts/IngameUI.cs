using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject inventoryUI;

    [SerializeField] private GameObject audioUI;
    [SerializeField] private GameObject graphicUI;
    [SerializeField] private GameObject saveUI;

    public bool sfxOn;
    public bool bgmOn;

    public void SettingOnOff()
    {
        settingUI.SetActive(!settingUI.activeSelf);
    }

    public void SFXOnOff()
    {
        sfxOn = !sfxOn;
    }

    public void BGMOnOff()
    {
        bgmOn = !bgmOn;
    }



    public void InventoryOnOff()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
