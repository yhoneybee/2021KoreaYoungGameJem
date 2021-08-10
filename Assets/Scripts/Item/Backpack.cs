using System.Collections;
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
    public void DiscardItem(Item item, bool isDrop = true, int Count = 1)
    {
        if (Count == 0)
            return;
        if (item)
        {
            if (item.IgnoreCountAttribute)
            {
                item.Count = 0;
                SetUi();
            }
            else
            {
                item.Count -= Count;
                if (item.Count <= 0)
                {
                    item.Count = 0;
                    SetUi();
                }
            }
            if (isDrop)
                DropItem(item).Count = Count;
        }
    }

    Item DropItem(Item item)
    {
        GameObject go = Instantiate(item.gameObject, PlayerInput.MousePos, Quaternion.identity);
        go.transform.localScale = Vector3.one;
        go.name = item.Name;
        go.AddComponent<ItemRotate>();
        go.AddComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        go.AddComponent<SpriteRenderer>().sprite = item.Icon;
        go.AddComponent<BoxCollider2D>().size = Vector2.one;

        return go.GetComponent<Item>();
    }

    public void SetUi(bool isFrist = false)
    {
        Item item, gm_item;

        for (int i = 0; i < GameManager.Instance.Items.Count; i++)
        {
            item = GameManager.Instance.Ui_Items[i];
            gm_item = GameManager.Instance.Items[i];

            if (isFrist)
                item.Count = gm_item.Count;

            item.ItemType = gm_item.ItemType;
            item.Icon = gm_item.Icon;
            item.Name = gm_item.Name;
            item.Info = gm_item.Info;
            item.IgnoreCountAttribute = gm_item.IgnoreCountAttribute;
        }
    }
}
