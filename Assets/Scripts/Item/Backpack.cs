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
        {
            gameObject.SetActive(false);
            Player.Instance.DayText.GetComponent<RectTransform>().anchoredPosition = new Vector3(40, -25, 0);
        }
        else
        {
            gameObject.SetActive(true);
            Player.Instance.DayText.GetComponent<RectTransform>().anchoredPosition = new Vector3(-625, -25, 0);
        }
    }

    /// <summary>
    /// 아이템을 얻는 함수
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item, int count = 1)
    {
        if (item)
        {
            if (item.IgnoreCountAttribute)
                item.Count = 1;
            else
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

    public Item DropItem(Item item)
    {
        GameObject go = Instantiate(item.gameObject, PlayerInput.MousePos, Quaternion.identity);
        go.transform.localScale = Vector3.one;
        go.transform.position += new Vector3(0, 0, 100);
        go.name = item.Name;
        go.AddComponent<ItemRotate>();
        go.AddComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        go.AddComponent<SpriteRenderer>().sprite = item.Icon;
        go.AddComponent<BoxCollider2D>().size = Vector2.one;

        return go.GetComponent<Item>();
    }

    public void SetUi(bool isFrist = false)
    {
        for (int i = 0; i < GameManager.Instance.Items.Count; i++)
            GameManager.Instance.Ui_Items[i].ItemAllocation(GameManager.Instance.Items[i], isFrist);
    }
}
