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

    int hunger = 8;
    public int Hunger
    {
        get { return hunger; }
        set 
        { 
            hunger = value;
            isDie = hunger == 0;
        }
    }

    int health = 10;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            isDie = health == 0;
        }
    }

    int moisture = 4;
    public int Moisture
    {
        get { return moisture; }
        set
        {
            moisture = value;
            isDie = moisture == 0;
        }
    }

    int day = 1;

    public bool isDie = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerInput = new PlayerInput(this);

        StartCoroutine(EDay());
        StartCoroutine(EConsumption());
    }

    private void Update()
    {
        PlayerInput.Update();

        if (Hunger > 8) Hunger = 8;
        if (Health > 10) Health = 10;
        if (Moisture > 4) Moisture = 4;
    }

    IEnumerator EDay()
    {
        while (true)
        {
            // 낮
            yield return new WaitForSeconds(60 * 10);
            // 밤
            yield return new WaitForSeconds(60 * 10);
            ++day;
        }
    }
    IEnumerator EConsumption()
    {
        while (true)
        {
            yield return new WaitForSeconds(60 * 5);
            --Hunger;
            --Moisture;
        }
    }
}
