using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public PlayerInput(Player player) => Player = player;

    Player Player { get; set; }

    Vector2 Dir { get; set; }
    Vector2 MousePos { get; set; }
    Quaternion Quaternion { get; set; }

    float Angle { get; set; }
    float Speed { get; set; } = 1f;
    bool BuildMode { get; set; } = false;
    void SetAngle()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Dir = MousePos - new Vector2(Player.transform.position.x, Player.transform.position.y);

        Angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        Quaternion = Quaternion.AngleAxis(Angle - 90, Vector3.forward);
    }

    void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (BuildMode)
            {
                // 분해
            }
            else
            {
                // 던지기
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (BuildMode)
            {
                // 짓기
            }
            else
            {
                // 상호작용키
            }
        }
    }

    void Move()
    {
        Vector2 move_dir = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
            move_dir += new Vector2(0, 1);
        if (Input.GetKey(KeyCode.A))
            move_dir += new Vector2(-1, 0);
        if (Input.GetKey(KeyCode.S))
            move_dir += new Vector2(0, -1);
        if (Input.GetKey(KeyCode.D))
            move_dir += new Vector2(1, 0);

        Player.transform.Translate(move_dir * Speed * Time.deltaTime);
    }

    void KeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 아이템 창 열기
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            // 멀티 시 구현
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            BuildMode = !BuildMode;
        }
    }

    public void Update()
    {
        SetAngle();

        MouseClick();

        Move();

        KeyBoard();
    }
}
