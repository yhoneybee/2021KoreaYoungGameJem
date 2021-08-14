using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public static Box OpenBox = null;

    public List<Item> BoxItems = new List<Item>();

    private void Start()
    {
        for (int i = 0; i < 14; i++)
            BoxItems.Add(new Item(ItemFactory.Instance.GetRandomItemData(), Random.Range(1, 11)));
    }
}
