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
}

[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public Sprite Icon = null;
    public Sprite Build = null;

    public ItemType ItemType = ItemType.MATERIAL;

    public int StackCount = 64;

    public string Name = "";
    [Multiline(5)]
    public string Info = "";

    public List<ItemData> Resources = null;
}
