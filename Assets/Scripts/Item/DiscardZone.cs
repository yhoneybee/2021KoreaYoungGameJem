using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DiscardZone : MonoBehaviour, IDropHandler
{
    public GameObject DropSeveral;
    Item Item;

    int val = 1;
    int Val
    {
        get { return val; }
        set
        {
            val = value;
            DropSeveral.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{val}";
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // TODO :  개수가 1개 이상이라면 여러개 버릴 수 있는 UI 띄우기
        if (Item.DraggingItem)
        {
            Val = Item.DraggingItem.Count;
            Item = Item.DraggingItem;
            DropSeveral.SetActive(true);
        }
    }

    public void Min() => Val = 0;
    public void Minus()
    {
        if (Val > 0)
            --Val;
    }
    public void Plus()
    {
        if (Item.GetComponent<ItemContainer>())
        {
            if (Val < PlayerInput.Box.Items.Find(o => o.ItemEqual(Item)).Count)
                ++Val;
        }
        else
        {
            if (Val < GameManager.Instance.Ui_Items.Find(o => o.ItemEqual(Item)).Count)
                ++Val;
        }
    }
    public void Max()
    {
        if (Item.GetComponent<ItemContainer>())
            Val = PlayerInput.Box.Items.Find(o => o.ItemEqual(Item)).Count;
        else
            Val = GameManager.Instance.Ui_Items.Find(o => o.ItemEqual(Item)).Count;
    }

    public void Trash()
    {
        if (Item.GetComponent<ItemContainer>())
        {
            Item trash = PlayerInput.Box.Items.Find(o => o.Name == Item.Name);
            if (trash)
            {
                trash.Count -= Val;

                Player.Instance.Backpack.DropItem(trash).Count = Val;

                if (trash.Count == 0)
                {
                    trash.ItemAllocation(new Item());
                    trash.enabled = false;
                    trash.GetComponent<ItemContainer>().enabled = true;
                }
            }
        }
        else
        {
            Player.Instance.Backpack.DiscardItem(GameManager.Instance.Ui_Items.Find(o => o.Name == Item.Name), true, Val);
        }
        DropSeveral.SetActive(false);
    }
}
