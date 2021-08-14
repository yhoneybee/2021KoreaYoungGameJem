using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;
    /// <summary>
    /// Resource Load 한 Items
    /// </summary>
    public List<Item> Items { get; set; } = new List<Item>();

    /// <summary>
    /// Scene에 있는 Items
    /// </summary>
    public List<Item> Ui_Items { get; set; } = new List<Item>();

    public GameObject ItemInfoWindow;

    public GameObject BoxWindow;

    public Player Player;

    bool mouse_over;

    public bool MouseOver
    {
        get 
        {
            return mouse_over;
        }
        set 
        {
            mouse_over = value;
            ItemInfoWindow.SetActive(mouse_over);
        }
    }

    public void SetItemInfo(Item item)
    {
        ItemInfoWindow.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.Data.Icon;
        ItemInfoWindow.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = item.Data.Name;
        ItemInfoWindow.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = $"{item.Count}개";
        ItemInfoWindow.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = item.Data.Info;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    }

    private void Update()
    {
        ItemInfoWindow.transform.position = InputManager.Instance.MousePos;
    }
}
