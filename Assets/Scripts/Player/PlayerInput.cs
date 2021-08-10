using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput
{
    public PlayerInput(Player player) => Player = player;

    Player Player { get; set; }

    Vector2 Dir { get; set; }
    public static Vector2 MousePos { get; private set; }
    Quaternion Quaternion { get; set; }

    public static GameObject Building { get; set; }

    float Angle { get; set; }
    float Speed { get; set; } = 3f;

    bool build_mode = false;
    bool BuildMode
    {
        get { return build_mode; }
        set
        {
            build_mode = value;
            if (build_mode)
            {
                Item select = Player.Hotbar[ItemContainer.SelectedIndex].Item;

                if (select && select.Count > 0 && select.ItemType == ItemType.HOUSE)
                {
                    Building = new GameObject(select.Name);
                    Building.AddComponent<SpriteRenderer>().sprite = select.Icon;
                    Building.GetComponent<SpriteRenderer>().material.color = new Color(0.3f, 1, 0.3f, 1);
                    Building.AddComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    Building.AddComponent<BoxCollider2D>().size = Vector2.one;
                }
            }
            else
            {
                if (Building)
                    Object.Destroy(Building);
            }
        }
    }

    void SetAngle()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Dir = MousePos - new Vector2(Player.transform.position.x, Player.transform.position.y);

        Angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        Quaternion = Quaternion.AngleAxis(Angle - 90, Vector3.forward);
    }

    void TempBuild()
    {
        if (BuildMode && Building)
        {
            Item select = Player.Hotbar[ItemContainer.SelectedIndex].Item;
            if (select && select.ItemType == ItemType.HOUSE)
            {
                Building.transform.position = MousePos;
            }
        }
    }

    void MouseClick()// Ray를 쏠 예정
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(MousePos, Vector3.forward, 50000);

            if (hit)
            {
                if (BuildMode)
                {
                    // 분해
                }
                else
                {
                    // 사용
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(MousePos, Vector3.forward, 50000);

            if (hit)
            {
                if (BuildMode && Building)
                {
                    Item item = Player.Hotbar[ItemContainer.SelectedIndex].Item;
                    // 짓기
                    if (item.ItemType == ItemType.HOUSE)
                    {
                        GameManager.Instance.Ui_Items.Find(o => o.Name == item.Name).Use();
                        BuildMode = false;
                    }
                }
                else
                {
                    // 상호작용키
                    Item item = hit.transform.gameObject.GetComponent<Item>();

                    if (item)
                    {
                        Item dragged = GameManager.Instance.Ui_Items.Find(o => o.Name == item.Name);

                        if (dragged)
                        {
                            Player.Backpack.AddItem(dragged, item.Count);

                            Object.Destroy(hit.transform.gameObject);
                            GameManager.Instance.MouseOver = false;
                        }
                    }
                }
            }
        }
    }

    void MouseWheel()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");

        if (wheel > 0)
        {
            // up
            BuildMode = false;
            ItemContainer.SelectedIndex++;
            ItemContainer.SelectedIndex %= 9;
        }
        else if (wheel < 0)
        {
            // down
            BuildMode = false;
            ItemContainer.SelectedIndex--;
            if (ItemContainer.SelectedIndex < 0) ItemContainer.SelectedIndex = 8;
        }

        Player.Instance.HotbarFrame.transform.localPosition = new Vector3(Player.Instance.Hotbar[ItemContainer.SelectedIndex].transform.localPosition.x, -465, 0);
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
            BuildMode = false;
            Player.Backpack.OpenAndClose();
            GameManager.Instance.ItemInfoWindow.SetActive(false);
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

        MouseWheel();

        Move();

        KeyBoard();

        TempBuild();
    }
}
