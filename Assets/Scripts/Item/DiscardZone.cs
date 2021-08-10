using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DiscardZone : MonoBehaviour, IDropHandler
{
    public GameObject DropSeveral;
    string Name;

    int val = 1;
    int Val
    {
        get { return val; }
        set 
        {
            val = value;
            DropSeveral.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{val}";
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        // TODO :  개수가 1개 이상이라면 여러개 버릴 수 있는 UI 띄우기
        Name = Item.DraggingItem.Name;
        DropSeveral.SetActive(true);
    }

    public void Min() => Val = 0;
    public void Minus()
    {
        if (Val > 0)
            --Val;
    }
    public void Plus()
    {
        if (Val < GameManager.Instance.Ui_Items.Find(o => o.Name == Name).Count)
            ++Val;
    }
    public void Max() => Val = GameManager.Instance.Ui_Items.Find(o => o.Name == Name).Count;
    public void Trash()
    {
        Player.Instance.Backpack.DiscardItem(GameManager.Instance.Ui_Items.Find(o => o.Name == Name), true, Val);
        DropSeveral.SetActive(false);
    }
}
