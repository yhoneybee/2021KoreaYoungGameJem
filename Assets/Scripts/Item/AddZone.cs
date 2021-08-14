using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // Dragging의 부모가 Content와 같지 않을 경우 ( Box에서나 Hotbar에서 드래그 해서 Drop한 경우 )
        if (UiItem.Dragging)
        {
            if (UiItem.Dragging.transform.parent != InventoryManager.Instance.Content.transform)
            {
                ItemData data = UiItem.Dragging.Item.Data;
                int count = UiItem.Dragging.Item.Count;

                InventoryManager.Instance.Sub(UiItem.Dragging, UiItem.Dragging.Item.Count, false, false);
                InventoryManager.Instance.Add(new Item(data, count));

                UiItem.Dragging.Item = null;
                UiItem.Dragging.SetUp();
            }
        }
    }
}
