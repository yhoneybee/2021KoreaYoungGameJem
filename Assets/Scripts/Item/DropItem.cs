using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public SpriteRenderer sr;

    public ItemData ItemData = null;

    private Item item;

    public Item Item
    {
        get { return item; }
        set 
        { 
            item = value;
            sr.sprite = item.Data.Icon;
        }
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        if (ItemData != null)
            Item = new Item(ItemData, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<DropItem>().Item;
        if (item != null)
        {
            Item.Count += item.Count;
            Destroy(collision.gameObject);
        }
    }
}
