using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public static Box OpenBox { get; set; } = null;

    public List<Item> BoxItems = new List<Item>();

    public bool VoidBox = false;

    private void Start()
    {
        if (VoidBox)
        {
            for (int i = 0; i < 14; i++)
                BoxItems.Add(new Item(null, 0));
        }
        else
        {
            for (int i = 0; i < 14; i++)
                BoxItems.Add(new Item(ItemFactory.Instance.GetRandomItemData(), Random.Range(1, 11)));
        }
    }
}
