using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } = null;
    public PlayerInput PlayerInput { get; set; }

    /// <summary>
    /// 밑에 있는 칸 9개
    /// </summary>
    public ItemContainer[] Hotbar = new ItemContainer[9];

    /// <summary>
    /// Hotbar Select Frame
    /// </summary>
    public GameObject HotbarFrame;

    public Backpack Backpack;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerInput = new PlayerInput(this);
    }

    private void Update()
    {
        PlayerInput.Update();
    }
}
