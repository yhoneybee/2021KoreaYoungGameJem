using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    THROW,
}

public enum ItemClass
{
    NONE,
    LOW,
    MIDDLE,
    HIGH,
}

[System.Serializable]
public class ItemCountPair
{
    public ItemData ItemData;
    public int Count;
}

[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    [Header(" - 사진")]
    [Space(10)]
    public Sprite Icon = null;
    public Sprite Ingame = null;

    [Header(" - 속성")]
    [Space(10)]
    public ItemType ItemType = ItemType.MATERIAL;
    public ItemClass ItemClass = ItemClass.NONE;

    [Header(" - 수치")]
    [Space(10)]
    public int StackCount = 64;
    public int Durability = 0;

    [Header(" - 정보")]
    [Space(10)]
    public string Name = "";
    [Multiline(5)]
    public string Info = "";

    [Header(" - 재료")]
    [Space(10)]
    public List<ItemCountPair> Resources = null;
}
