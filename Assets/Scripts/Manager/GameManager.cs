using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;
    public List<Item> Items { get; set; } = new List<Item>();

    public GameObject ItemInfoWindow;

    public bool MouseOver { get; set; }

    /// <summary>
    /// Key : 조합해서 완성하는 아이템
    /// Value : 조합하기 위해 필요한 아이템
    /// </summary>
    public Dictionary<Item, List<Item>> CraftItem = new Dictionary<Item, List<Item>>
    {

    };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Items.AddRange(Resources.LoadAll<Item>("Item/"));
        Player.Instance.Backpack.SetUi(true);
    }

    private void Update()
    {
        if (MouseOver)
            ItemInfoWindow.transform.position = PlayerInput.MousePos;
    }
}
