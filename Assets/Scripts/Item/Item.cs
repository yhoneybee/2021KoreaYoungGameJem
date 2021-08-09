using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
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
    public Sprite Icon { get; set; }

    public int Name { get; set; }

    /// <summary>
    /// 아이템의 정보
    /// </summary>
    public string Info { get; set; }

    /// <summary>
    /// Count가 1일때는 UI에 표시하지 않고, 아이콘만 표시
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// true라면 Count의 속성이 무시되며 아이템을 겹칠 수 없다.
    /// </summary>
    public bool IgnoreCountAttribute { get; set; }

    /// <summary>
    /// 아이템을 사용함 ( 무기 -> 장착, 건물 -> 건설, 소모품 -> 사용 )
    /// </summary>
    public abstract void Use();

    private void Update()
    {
        if (IgnoreCountAttribute)
            Count = 1;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOverItem = this;
        // 아이템 정보창 띄우기
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseOverItem = null;
        // 아이템 정보창 지우기
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Use();
    }
}
