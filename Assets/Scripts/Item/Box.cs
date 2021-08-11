using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Box : MonoBehaviour
{
    public int ItemCount;

    /// <summary>
    /// 상자안에 있는 아이템들
    /// </summary>
    public List<Item> Items { get; set; } = new List<Item>();

    private void Start()
    {
        Items.AddRange(GameManager.Instance.BoxWindow.GetComponentsInChildren<Item>());
        for (int i = 0; i < ItemCount; i++)
            Items[Random.Range(0, 14)].ItemAllocation(GameManager.Instance.Ui_Items[Random.Range(0, GameManager.Instance.Ui_Items.Count)]);
    }

    public void OpenAndCloseBox(bool open)
    {
        GameManager.Instance.BoxWindow.SetActive(open);

        if (open)
        {
            Item item;

            for (int i = 0; i < 14; i++)
            {
                item = GameManager.Instance.BoxWindow.transform.GetChild(i).GetComponent<Item>();

                if (Items[i] && Items[i].Name == "")
                {
                    item.enabled = false;
                    continue;
                }

                item.GetComponent<ItemContainer>().enabled = false;

                if (Items[i])
                {
                    item.ItemAllocation(Items[i]);
                }

                if (item.Count == 0)
                    item.Count = Random.Range(1, 15);
            }
        }
    }
}
