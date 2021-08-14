using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UiItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private Item item;
    public Image Image;
    public TextMeshProUGUI Count;

    public Item Item
    {
        get { return item; }
        set { item = value; SetUp(); }
    }

    public static UiItem MouseOver { get; set; } = null;
    public static UiItem MouseClick { get; set; } = null;
    public static UiItem Dragging { get; set; } = null;

    public void SetUp()
    {
        if (item != null && item.Data != null)
        {
            Image.sprite = item.Data.Icon;
            Count.text = $"{(item.Count > 0 ? item.Count.ToString() : "")}";
        }
        else
        {
            Image.sprite = null;
            Count.text = $"";
        }
    }

    private void OnEnable()
    {
        SetUp();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null && item.Data != null)
        {
            MouseOver = this;
            GameManager.Instance.SetItemInfo(item);
            GameManager.Instance.MouseOver = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseOver = null;
        GameManager.Instance.MouseOver = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MouseClick = this;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Dragging)
            Dragging.transform.position = InputManager.Instance.MousePos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null && item.Count > 0)
        {
            Dragging = this;
            Dragging.Image.raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Dragging)
        {
            Dragging.Image.raycastTarget = true;

            InventoryManager.Instance.Content.enabled = false;
            InventoryManager.Instance.Content.enabled = true;

            InventoryManager.Instance.BoxLayout.enabled = false;
            InventoryManager.Instance.BoxLayout.enabled = true;

            GameManager.Instance.Player.GLG.enabled = false;
            GameManager.Instance.Player.GLG.enabled = true;

            Dragging = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Item != null && Item.Data == Dragging.Item.Data)
        {
            InventoryManager.Instance.Add(Item);
            Destroy(Dragging.gameObject);
        }
        else // 드롭한곳의 Item이 Null인경우 ( Hotbar, Box )
        {
            Item = new Item(Dragging.Item);
            if (Dragging.transform.parent == InventoryManager.Instance.Content.transform)
            {
                print("Hotbar, Box -> Destroy");
                InventoryManager.Instance.Sub(Dragging, Dragging.Item.Count, false, false);
                Destroy(Dragging.gameObject);
            }
            else
            {
                print("Hotbar, Box -> Sub");
                InventoryManager.Instance.Sub(Dragging, Dragging.Item.Count, false, false);

            }
        }

        SetUp();
    }
}
