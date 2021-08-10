using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } = null;
    public PlayerInput PlayerInput { get; set; }

    public Backpack Backpack;

    /// <summary>
    /// 35.9 ℃ ~ 37.6 ℃가 정상적인 체온 범위 이다
    /// </summary>
    public float Temperature { get; set; } = 36.5f;

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
