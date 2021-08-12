﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemContainer : MonoBehaviour, IDropHandler
{
    Item item;
    public Item Item
    {
        get { return item; }
        set
        {
            item = value;
            if (item)
                GetComponent<Image>().sprite = item.Icon;
            else
                GetComponent<Image>().sprite = null;
        }
    }

    /// <summary>
    /// 핫바에서 선택된 아이템 칸
    /// </summary>
    static int selected_index;
    public static int SelectedIndex
    {
        get { return selected_index; }
        set
        {
            selected_index = value;
            selected_index %= 9;
            if (selected_index < 0) selected_index = 8;

            Item selected = Player.Instance.Hotbar[selected_index].item;
            if (selected)
                Player.Instance.ItemName.text = $"{selected.Name}";
            else
                Player.Instance.ItemName.text = $"";
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Item.DraggingItem)
        {
            Item = Item.DraggingItem;

            print("item");
            //if (Item.DraggingItem.transform.parent.name == "BoxWindow")
            {
                Item gitem = GetComponent<Item>();
                if (gitem)
                {
                    print("gitem");

                    gitem.ItemAllocation(Item);
                    gitem.enabled = true;
                    enabled = false;

                    for (int i = 0; i < PlayerInput.Box.Items.Count; i++)
                    {
                        if (!PlayerInput.Box.Items[i]) continue;

                        if (PlayerInput.Box.Items[i].Name == Item.Name)
                        {
                            if (gitem != Item.DraggingItem)
                            {
                                PlayerInput.Box.Items[i] = gitem;
                                break;
                            }
                        }
                    }

                    ItemContainer ic = Item.DraggingItem.GetComponent<ItemContainer>();

                    Box box = PlayerInput.Box.GetComponent<Box>();

                    int a = System.Convert.ToInt32(name);

                    if (ic)
                    {
                        print("ic");

                        Item.DraggingItem.enabled = false;
                        ic.Item = null;
                        ic.enabled = true;
                        Item.DraggingItem.ItemAllocation(new Item());

                        int r = System.Convert.ToInt32(Item.DraggingItem.name);

                        box.idxs.Remove(r);
                    }

                    enabled = false;
                    gitem.enabled = true;

                    box.idxs.Add(a);
                    print(a);

                    try
                    {
                        box.Items[a] = GetComponent<Item>();
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e.Message);
                    }

                    Item.DraggingItem.GetComponent<Image>().raycastTarget = true;
                    Item.DraggingItem.Count = 0;
                    if (Item.DraggingItem.transform.parent.name == "BoxWindow")
                        Item.DraggingItem.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                else if (Item.transform.parent.name == "BoxWindow")
                    Item = null;
            }
        }
    }

    private void Update()
    {
        if (item && item.Count == 0)
        {
            item = null;
            GetComponent<Image>().sprite = null;
        }
    }
}
