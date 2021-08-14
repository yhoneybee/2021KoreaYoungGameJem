using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public ItemData ItemData;
    public List<ItemData> BonusItems;
    public SpriteRenderer sr;

    private Item item;

    public Item Item
    {
        get { return item; }
        set
        {
            item = value;
            sr.sprite = item.Data.Ingame;
        }
    }

    private void Start()
    {
        Item = new Item(ItemData, 1);
    }

    public void Collect()
    {
        var tool = GameManager.Instance.Player.Hotbar[GameManager.Instance.Player.HotbarIndex];
        if (tool.Item != null && tool.Item.Data.ItemType == ItemType.UNIVERSAL)
        {
            if ((int)tool.Item.Data.ItemClass >= (int)Item.Data.ItemClass)
            {
                GameManager.Instance.Player.sr.flipX = InputManager.Instance.MousePos.x > GameManager.Instance.Player.transform.position.x;
                SoundManager.Instance.Play("e_hitting_hitting_1");
                GameManager.Instance.Player.Animator.SetTrigger("Collect");
                tool.Item.Use();
                InventoryManager.Instance.Add(Item);
                BonusItem();
                Destroy(gameObject);
            }
        }
    }

    public void BonusItem()
    {
        if (Random.Range(0, 10) > 5)
        {
            foreach (var item in BonusItems)
                InventoryManager.Instance.Add(new Item(item, 1));
        }
    }
}
