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
        if (item.Data != null)
            Durability = item.Data.Durability;
    }
    public Item(ItemData data, int count)
    {
        Data = data;
        Count = count;
        if (data != null)
            Durability = data.Durability;
    }

    public ItemData Data;

    private int count;

    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    private int durability = 0;

    public int Durability
    {
        get { return durability; }
        set { durability = value; }
    }

    public void Use()
    {
        switch (Data.ItemType)
        {
            case ItemType.MATERIAL:
                Debug.LogWarning("[ 금지된 접근입니다! ]");
                break;
            case ItemType.FOOD:
                if (Data.Name == "물")
                    GameManager.Instance.Player.PlayerState.Moisture++;
                else
                    GameManager.Instance.Player.PlayerState.Hunger++;
                break;
            case ItemType.TREATMENT:
                GameManager.Instance.Player.PlayerState.Health++;
                break;
            case ItemType.TRAP:
                break;
            case ItemType.TOOL:
                --Durability;
                break;
            case ItemType.UNIVERSAL:
                --Durability;
                break;
            case ItemType.WATER_PURIFIER:
                --Durability;
                break;
            case ItemType.HOUSE:
                var house = Object.Instantiate(InputManager.Instance.Building, InputManager.Instance.MousePos, Quaternion.identity);
                house.transform.position = new Vector3(house.transform.position.x, house.transform.position.y, 101);
                house.Placed = true;
                break;
            case ItemType.FRAME:
                break;
            case ItemType.CLOTHES:
                Debug.LogWarning("[ 금지된 접근입니다! ]");
                break;
            case ItemType.THROW:
                Object.Instantiate(GameManager.Instance.Throw,
                                   GameManager.Instance.Player.transform.position,
                                   InputManager.Instance.Quaternion)
                    .ItemData = GameManager.Instance.Player.Hotbar[GameManager.Instance.Player.HotbarIndex].Item.Data;
                break;
            case ItemType.END:
                SceneController.Instance.Hidden = true;
                break;
        }

        if (Durability <= 0)
            InventoryManager.Instance.Sub(GameManager.Instance.Player.Hotbar[GameManager.Instance.Player.HotbarIndex], 1, false, false);
    }
}
