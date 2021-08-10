using System.Collections;
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
            GetComponent<Image>().sprite = item.Icon;
        }
    }

    /// <summary>
    /// 핫바에서 선택된 아이템 칸
    /// </summary>
    public static int SelectedIndex { get; set; }

    public void OnDrop(PointerEventData eventData)
    {
        if (Item.DraggingItem)
        {
            Item = Item.DraggingItem;
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
