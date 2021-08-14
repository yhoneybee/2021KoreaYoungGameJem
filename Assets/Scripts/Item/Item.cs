using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public Item(Item item)
    {
        Data = item.Data;
        Count = item.Count;
    }
    public Item(ItemData data, int count)
    {
        Data = data;
        Count = count;
    }

    public ItemData Data;

    private int count;

    public int Count
    {
        get { return count; }
        set 
        {
            count = value;
        }
    }

    public void Use()
    {
        switch (Data.ItemType)
        {
            case ItemType.MATERIAL:
                Debug.LogWarning("[ 금지된 접근입니다! ]");
                break;
            case ItemType.FOOD:
                break;
            case ItemType.TREATMENT:
                break;
            case ItemType.TRAP:
                break;
            case ItemType.TOOL:
                break;
            case ItemType.UNIVERSAL:
                break;
            case ItemType.WATER_PURIFIER:
                break;
            case ItemType.HOUSE:
                break;
            case ItemType.FRAME:
                break;
            case ItemType.CLOTHES:
                Debug.LogWarning("[ 금지된 접근입니다! ]");
                break;
        }
    }
}
