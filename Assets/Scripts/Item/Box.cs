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
    public List<int> idxs = new List<int>();

    private void Start()
    {
        Items.Add(new Item());
        Items.AddRange(GameManager.Instance.BoxWindow.GetComponentsInChildren<Item>());

        int rand_item = Random.Range(0, GameManager.Instance.Ui_Items.Count - 2);

        List<int> overlap = new List<int>();
        for (int i = 0; i < GameManager.Instance.Ui_Items.Count - 2; i++)
            overlap.Add(i);

        for (int i = 0; i < ItemCount; i++)
        {
            int rand = Random.Range(1, 15);

            while (overlap[rand_item] != rand_item)
                rand_item = Random.Range(0, GameManager.Instance.Ui_Items.Count - 2);

            overlap[rand_item] = -9999;
            Items[rand].ItemAllocation(GameManager.Instance.Ui_Items[rand_item]);
            Items[rand].Count = 1;
            idxs.Add(rand);
        }
    }

    public void OpenAndCloseBox(bool open)
    {
        GameManager.Instance.BoxWindow.SetActive(open);

        if (open)
        {
            Item item;

            for (int i = 1; i < Items.Count; i++)
            {
                item = GameManager.Instance.BoxWindow.transform.GetChild(i - 1).GetComponent<Item>();

                if (idxs.Find(o => o == i) == 0)
                {
                    item.enabled = false;
                    item.GetComponent<ItemContainer>().enabled = true;
                    item.GetComponent<Image>().sprite = null;
                }
                else
                {
                    item.enabled = true;
                    item.GetComponent<ItemContainer>().enabled = false;

                    try
                    {
                        item.ItemAllocation(Items[i]);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e.Message);
                    }
                }
            }
        }
    }
}
