using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
            IsDie = health == 0;
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
            IsDie = hunger == 0;
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
            IsDie = moisture == 0;
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

    void SetDayText() => DayText.text = $"{day}days - {(isday ? "day" : "night")}";

    private bool is_die;

    public bool IsDie
    {
        get { return is_die; }
        set 
        {
            is_die = value;
            if (value)
            {
                SceneManager.LoadScene("EndingScene");
            }
        }
    }


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
            yield return new WaitForSeconds(60 * 10);
            // 밤
            IsDay = false;
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
