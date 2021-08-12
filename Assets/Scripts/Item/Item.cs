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

    [SerializeField]
    private Sprite build_sprite;

    public Sprite BuildSprite
    {
        get { return build_sprite; }
        set { build_sprite = value; }
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
        Player.Instance.Backpack.DiscardItem(this, false);
        switch (ItemType)
        {
            //재료 사용
            case ItemType.MATERIAL:
                {
                    // 그냥 재료는 Use못함
                    Debug.LogError("재료를 어따 쓰게 친구?");
                }
                break;
            //음식 섭취
            case ItemType.FOOD:
                {
                    Player.Instance.Hunger++;
                }
                break;
            //치료
            case ItemType.TREATMENT:
                {
                    Player.Instance.Health++;
                }
                break;
            //덫
            case ItemType.TRAP:
                {
                    Instantiate(PlayerInput.Building, PlayerInput.MousePos, Quaternion.identity);
                    // TODO : Trap 클래스 추가
                }
                break;
            //도구
            case ItemType.TOOL:
                {
                    // TODO : Attack 연결하기 히히
                }
                break;
            //만능
            case ItemType.UNIVERSAL:
                {
                    // TODO : 채집?
                }
                break;
            //정수기
            case ItemType.WATER_PURIFIER:
                {
                    // TODO : 이거 물어봐야지 ㅎㅋ
                }
                break;
            //집
            case ItemType.HOUSE:
                {
                    GameObject go = Instantiate(PlayerInput.Building, PlayerInput.MousePos, Quaternion.identity);
                    go.GetComponent<Build>().Placed = true;
                    go.transform.localPosition += new Vector3(0, 0, 100);
                    // TODO : House 클래스 추가 ( 이거 PlayerInput.Building에서 추가함 )
                }
                break;
            //틀
            case ItemType.FRAME:
                {
                    // 이 이친구도 설치 류인가?
                    // 그렇다면 TODO : Frame 클래스 (아마 Trap이랑 같은 클래스 사용할듯)
                    Instantiate(PlayerInput.Building, PlayerInput.MousePos, Quaternion.identity);
                }
                break;
            //옷
            case ItemType.CLOTHES:
                {
                    Debug.LogWarning("[ 금지된 접근입니다! ]");
                }
                break;
        }
    }

    private void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOverItem = this;
        //if (!DraggingItem)
        {
            SetupItemInfoWindow(MouseOverItem);
            GameManager.Instance.MouseOver = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseOverItem = null;
        GameManager.Instance.MouseOver = false;
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
        if (count > 0)
        {
            DraggingItem = this;
            GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (DraggingItem)
        {
            GetComponent<Image>().raycastTarget = true;
            Player.Instance.Backpack.Content.GetComponent<GridLayoutGroup>().constraintCount = 6;
            Player.Instance.Backpack.Content.GetComponent<GridLayoutGroup>().constraintCount = 5;

            GameManager.Instance.BoxWindow.GetComponent<GridLayoutGroup>().constraintCount = 3;
            GameManager.Instance.BoxWindow.GetComponent<GridLayoutGroup>().constraintCount = 2;

            DraggingItem.transform.localPosition = new Vector3(DraggingItem.transform.position.x, DraggingItem.transform.position.y, 0);

            DraggingItem = null;
        }
    }

    public void SetupItemInfoWindow(Item item)
    {
        GameObject window = GameManager.Instance.ItemInfoWindow;
        window.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.icon;
        window.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = item.Name;
        window.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = $"{item.Count}개";
        window.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = item.Info;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void ItemAllocation(Item item, bool count = true)
    {
        if (count)
            Count = item.Count;
        ItemType = item.ItemType;
        Icon = item.Icon;
        BuildSprite = item.BuildSprite;
        Name = item.Name;
        Info = item.Info;
        IgnoreCountAttribute = item.IgnoreCountAttribute;
    }

    public bool ItemEqual(Item item)
    {
        return (Count == item.Count && Name == item.Name);
    }
}
