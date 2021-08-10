using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiscardZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // 개수가 1개 이상이라면 여러개 버릴 수 있는 UI 띄우기
        Player.Instance.Backpack.DiscardItem(Item.DraggingItem);
    }
}
