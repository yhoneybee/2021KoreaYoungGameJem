using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float Speed { get; set; } = 3;

    public GridLayoutGroup GLG;

    public UiItem[] Hotbar = new UiItem[9];

    private int hotbar_index;

    public int HotbarIndex
    {
        get { return hotbar_index; }
        set
        {
            hotbar_index = value;
            hotbar_index %= 9;
            if (hotbar_index < 0) hotbar_index = 8;
        }
    }


    public TextMeshProUGUI DayText;

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            InventoryManager.Instance.Add(new Item(ItemFactory.Instance.GetRandomItemData(), Random.Range(1, 6)));
        }
    }
}
