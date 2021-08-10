﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class Backpack : MonoBehaviour
{
    public GameObject Content;

    private void Start()
    {
    }

    public void OpenAndClose()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    /// <summary>
    /// 아이템을 얻는 함수
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item, int count = 1)
    {
        if (item)
        {
            if (!item.IgnoreCountAttribute)
                item.Count += count;

            SetUi();
        }
    }

    /// <summary>
    /// 아이템을 일부만 버리는 함수
    /// </summary>
    /// <param name="item">버릴려는 아이템</param>
    /// <param name="Count">버릴 개수</param>
    public void DiscardItem(Item item, int Count = 1)
    {
        if (item)
        {
            item.Count -= Count;
            if (item.Count <= 0)
            {
                item.Count = 0;
                SetUi();
            }
            DropItem(item).Count = Count;
        }
    }

    Item DropItem(Item item)
    {
        GameObject go = Instantiate(item.gameObject, PlayerInput.MousePos, Quaternion.identity);
        go.transform.SetParent(GameObject.Find("Canvas").transform);
        go.GetComponent<RectTransform>().sizeDelta = Vector2.one * 100;
        go.transform.localScale = Vector3.one;
        go.name = item.Name;
        go.AddComponent<ItemRotate>();
        go.GetComponent<Image>().raycastTarget = true;

        return go.GetComponent<Item>();
    }

    public void SetUi(bool isFrist = false)
    {
        Item item, gm_item;

        for (int i = 0; i < GameManager.Instance.Items.Count; i++)
        {
            item = Content.transform.GetChild(i).GetComponent<Item>();
            gm_item = GameManager.Instance.Items[i];

            if (isFrist)
            {
                item.Count = gm_item.Count;
                item.IgnoreCountAttribute = gm_item.IgnoreCountAttribute;
            }
            item.ItemType = gm_item.ItemType;
            item.Icon = gm_item.Icon;
            item.Name = gm_item.Name;
            item.Info = gm_item.Info;
        }
    }
}
