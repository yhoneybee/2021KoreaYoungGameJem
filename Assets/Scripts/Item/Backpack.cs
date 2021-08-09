using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Backpack : MonoBehaviour
{
    const int MAX_COUNT = 30;
    public readonly List<Item> items = new List<Item>();

    public void OpenAndClose()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    public void AddItem(Item item)
    {
        if (item)
        {
            if (items.Count <= MAX_COUNT)
            {
                items.Add(item);
            }
            else
            {
                Debug.LogWarning($"가방 최대치에 도달하였습니다.");//라고 띄우기
            }
        }
    }

    public void DiscardItem(Item item)
    {
        if (item)
        {
            items.Remove(item);
        }
    }
}
