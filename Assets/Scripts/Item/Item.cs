using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum ItemType
{
    MATERIAL,
    FOOD,
    TREATMENT,
    TRAP,
    TOOL,
    UNIVERSAL,
    WATER_PURIFIER,
    HOUSE,
    FRAME,
    CLOTHES,
}

[System.Serializable]
public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public ItemType ItemType;

    /// <summary>
    /// 마우스가 위에 있는 Item
    /// </summary>
    public static Item MouseOverItem { get; set; } = null;

    /// <summary>
    /// 드레그 중인 Item
    /// </summary>
    public static Item DraggingItem { get; set; } = null;

    /// <summary>
    /// 아이템의 아이콘
    /// </summary>
    [SerializeField]
    Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
        set
        {
            icon = value;
            GetComponent<Image>().sprite = icon;
        }
    }

    public string Name;

    /// <summary>
    /// 아이템의 정보
    /// </summary>
    public string Info;

    /// <summary>
    /// Count가 1일때는 UI에 표시하지 않고, 아이콘만 표시
    /// </summary>
    [SerializeField]
    int count = 1;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            if (count > 0)
                GetComponent<Image>().color = new Color(1, 1, 1, 1);
            else
                GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    /// <summary>
    /// true라면 Count의 속성이 무시되며 아이템을 겹칠 수 없다.
    /// </summary>
    public bool IgnoreCountAttribute = false;

    public void Use()
    {
        switch (ItemType)
        {
            //재료 사용
            case ItemType.MATERIAL: break;
            //음식 섭취
            case ItemType.FOOD: break;
            //치료
            case ItemType.TREATMENT: break;
            //덫
            case ItemType.TRAP: break;
            //도구
            case ItemType.TOOL: break;
            //만능
            case ItemType.UNIVERSAL: break;
            //정수기
            case ItemType.WATER_PURIFIER: break;
            //집
            case ItemType.HOUSE: break;
            //틀
            case ItemType.FRAME: break;
            //옷
            case ItemType.CLOTHES: break;
        }
    }

    private void Update()
    {
        if (IgnoreCountAttribute)
            Count = 1;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOverItem = this;
        if (!DraggingItem)
        {
            GameManager.Instance.MouseOver = true;

            SetupItemInfoWindow();

            GameManager.Instance.ItemInfoWindow.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseOverItem = null;
        GameManager.Instance.ItemInfoWindow.SetActive(false);
        GameManager.Instance.MouseOver = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Use();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (DraggingItem)
        {
            DraggingItem.transform.position = PlayerInput.MousePos;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DraggingItem = this;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        Player.Instance.Backpack.Content.GetComponent<GridLayoutGroup>().constraintCount = 6;
        Player.Instance.Backpack.Content.GetComponent<GridLayoutGroup>().constraintCount = 5;
        DraggingItem.transform.localPosition = new Vector3(DraggingItem.transform.position.x, DraggingItem.transform.position.y, 0);
        DraggingItem = null;
    }

    public void SetupItemInfoWindow()
    {
        GameObject window = GameManager.Instance.ItemInfoWindow;
        window.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = MouseOverItem.icon;
        window.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = MouseOverItem.Name;
        window.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = $"{MouseOverItem.Count}개";
        window.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = MouseOverItem.Info;
    }
}
