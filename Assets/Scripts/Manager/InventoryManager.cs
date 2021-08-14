using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance = null;

    public GameObject Inventory;
    public GridLayoutGroup Content;
    public GridLayoutGroup BoxLayout;
    public UiItem UiItem;
    public DropItem DropItem;

    public List<Item> MyItems = new List<Item>();

    private bool show_inventory = false;

    public bool ShowInventory
    {
        get { return show_inventory; }
        set
        {
            show_inventory = value;
            Inventory.SetActive(value);

            if (value)
                GameManager.Instance.Player.DayText.GetComponent<RectTransform>().anchoredPosition = new Vector3(-625, -25, 0);
            else
                GameManager.Instance.Player.DayText.GetComponent<RectTransform>().anchoredPosition = new Vector3(40, -25, 0);
        }
    }

    private bool show_box = false;

    public bool ShowBox
    {
        get { return show_box; }
        set
        {
            show_box = value;
            BoxLayout.gameObject.SetActive(value);

            if (value)
            {
                for (int i = 0; i < 14; i++)
                {
                    var item = BoxLayout.transform.GetChild(i).GetComponent<UiItem>();
                    if (Box.OpenBox.BoxItems[i].Data != null)
                    {
                        item.Item = new Item(Box.OpenBox.BoxItems[i]);
                        item.SetUp();
                    }
                }
            }
        }
    }


    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Item find = MyItems.Find(o => o.Data == item.Data && o.Count <= o.Data.StackCount);
        if (find != null)
        {
            find.Count += item.Count;
            if (find.Count > find.Data.StackCount)
            {
                Add(new Item(find.Data, find.Count - find.Data.StackCount));
                find.Count = find.Data.StackCount;
            }
        }
        else
        {
            if (item.Data != null && item.Count > 0)
            {
                var ui = Instantiate(UiItem);
                ui.transform.SetParent(Content.transform);
                ui.transform.localScale = Vector3.one;

                ui.Item = item;
                MyItems.Add(item);
            }
        }
    }

    public void Sub(UiItem ui_item, int count = 1, bool drop = false, bool destroy = true)
    {
        if (ui_item.Item == null) return;

        ItemData data = ui_item.Item.Data;

        int drop_count = 0;

        ui_item.Item.Count -= count;

        if (ui_item.Item.Count > 0) // 빼도 0보다 클때
        {
            drop_count += count;
        }
        else // 0보다 작을 때
        {
            int abs = Mathf.Abs(ui_item.Item.Count);
            ui_item.Item.Count += abs;

            drop_count += count - abs;

            for (int i = MyItems.Count - 1; i >= 0; i--)
                if (MyItems[i].Data == ui_item.Item.Data && MyItems[i].Count == ui_item.Item.Count)
                    MyItems.Remove(MyItems[i]);

            ui_item.Item = null;

            if (destroy)
                Destroy(ui_item.gameObject);
        }

        if (drop && drop_count > 0)
        {
            var sprite = Instantiate(DropItem);
            sprite.Item = new Item(data, drop_count);
            sprite.transform.position = GameManager.Instance.Player.transform.position + new Vector3(0, -1, 0);
        }

        ui_item.SetUp();
    }
}
