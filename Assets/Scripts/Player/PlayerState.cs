using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerState : MonoBehaviour
{
    public Slider[] bars = new Slider[3];

    public TextMeshProUGUI DayText;

    public TextMeshProUGUI ItemName;

    int health = 10;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            bars[0].value = health;
            isDie = health == 0;
        }
    }

    int hunger = 8;
    public int Hunger
    {
        get { return hunger; }
        set
        {
            hunger = value;
            bars[1].value = hunger;
            isDie = hunger == 0;
        }
    }


    int moisture = 4;
    public int Moisture
    {
        get { return moisture; }
        set
        {
            moisture = value;
            bars[2].value = moisture;
            isDie = moisture == 0;
        }
    }

    int day = 1;
    public int Day
    {
        get { return day; }
        set
        {
            day = value;
            SetDayText();
        }
    }

    bool isday = true;
    public bool IsDay
    {
        get { return isday; }
        set
        {
            isday = value;
            SetDayText();
        }
    }

    void SetDayText() => DayText.text = $"{day}일 {(isday ? "낮" : "밤")}";

    public bool isDie = false;

    private void Start()
    {
        Health = 10;
        Hunger = 8;
        Moisture = 4;

        StartCoroutine(EDay());
        StartCoroutine(EConsumption());
    }

    private void Update()
    {
        if (Hunger > 8) Hunger = 8;
        if (Health > 10) Health = 10;
        if (Moisture > 4) Moisture = 4;

        var item = GameManager.Instance.Player.Hotbar[GameManager.Instance.Player.HotbarIndex].Item;
        if (item != null && item.Data != null)
            ItemName.text = $"{item.Data.Name}";
        else
            ItemName.text = $"";
    }

    IEnumerator EDay()
    {
        while (true)
        {
            // 낮
            IsDay = true;
            yield return new WaitForSeconds(10);
            //yield return new WaitForSeconds(60 * 10);
            // 밤
            IsDay = false;
            yield return new WaitForSeconds(10);
            //yield return new WaitForSeconds(60 * 10);
            ++day;
        }
    }
    IEnumerator EConsumption()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            //yield return new WaitForSeconds(60 * 5);
            --Hunger;
            --Moisture;
        }
    }
}
