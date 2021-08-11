using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool mouse_over;

    public bool MouseOver
    {
        get { return mouse_over; }
        set { mouse_over = value; ItemInfoWindow.SetActive(mouse_over); }
    }

    /// <summary>
    /// Key : 조합해서 완성하는 아이템 이름
    /// Value : { 필요 아이템 이름, 필요 아이템 개수 }, ...
    /// </summary>
    public Dictionary<string, List<Tuple<string, int>>> CraftItem = new Dictionary<string, List<Tuple<string, int>>>
    {
        {"덫(중급)",new List<Tuple<string, int>>{ new Tuple<string, int>("돌",3), } },
        {"덫(상급)",new List<Tuple<string, int>>{ new Tuple<string, int>("철",2), } },
        {"칼(중급)",new List<Tuple<string, int>>{ new Tuple<string, int>("돌",5), } },
        {"칼(상급)",new List<Tuple<string, int>>{ new Tuple<string, int>("철",10), } },
        {"도끼(하급)",new List<Tuple<string, int>>{ new Tuple<string, int>("돌",3), } },
        {"도끼(중급)",new List<Tuple<string, int>>{ new Tuple<string, int>("돌",5), } },
        {"도끼(상급)",new List<Tuple<string, int>>{ new Tuple<string, int>("철",10), } },
        {"정수기",new List<Tuple<string, int>>{ new Tuple<string, int>("자갈",10), } },
        {"텐트",new List<Tuple<string, int>>{ new Tuple<string, int>("덩쿨(중급)",10), } },
        {"나무집",new List<Tuple<string, int>>{ new Tuple<string, int>("덩쿨(상급)",50), } },
        {"틀",new List<Tuple<string, int>>{ new Tuple<string, int>("덩쿨(중급)",3), } },
    };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Items.AddRange(Resources.LoadAll<Item>("Item/"));
        Ui_Items.AddRange(Player.Instance.Backpack.Content.GetComponentsInChildren<Item>());
        Player.Instance.Backpack.SetUi(true);
    }

    private void Update()
    {
        ItemInfoWindow.transform.position = PlayerInput.MousePos;
    }
}
