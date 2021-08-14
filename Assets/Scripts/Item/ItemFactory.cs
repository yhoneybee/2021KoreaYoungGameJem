using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public static ItemFactory Instance;
    public List<ItemData> ItemDatas;

    private void Awake()
    {
        Instance = this;
    }

    public ItemData GetRandomItemData()
    {
        if (Random.Range(0, 10) < 5)
            return ItemDatas[Random.Range(0, ItemDatas.Count)];
        else
            return null;
    }
}
