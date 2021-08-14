using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance = null;

    public float Horizontal => Input.GetAxis("Horizontal");
    public float Vertical => Input.GetAxis("Vertical");
    public Vector2 MousePos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private bool build_mode;

    public GameObject HotbarFrame;

    public bool BuildMode
    {
        get { return build_mode; }
        set { build_mode = value; }
    }

    Player Player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Player = GameManager.Instance.Player;
    }

    void MouseDownLeft()
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
            }
            else
            {
                // 사용
                //Item item = Player.Hotbar[ItemContainer.SelectedIndex].Item;

                //if (!Player.Instance.Backpack.gameObject.activeSelf &&
                //    !GameManager.Instance.ItemInfoWindow.activeSelf &&
                //    !GameManager.Instance.BoxWindow.activeSelf)
                //{
                //    if (item && item.ItemType != ItemType.HOUSE)
                //        item.Use();
                //}
            }
        }
    }

    void MouseDownRight()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(MousePos, Vector3.forward, 50000);

            if (hit)
            {
                if (BuildMode)
                {
                    // 짓기
                }
                else
                {
                    // 상호작용

                    DropItem item = hit.transform.gameObject.GetComponent<DropItem>();
                    Box box = hit.transform.gameObject.GetComponent<Box>();

                    if (item != null)
                    {
                        InventoryManager.Instance.Add(new Item(item.Item.Data, item.Item.Count));
                        GameManager.Instance.MouseOver = false;
                        Destroy(hit.transform.gameObject);
                    }
                    else if (box)
                    {
                        Box.OpenBox = box;
                        InventoryManager.Instance.ShowBox = !InventoryManager.Instance.ShowBox;
                    }
                }
                //if (BuildMode && Building)
                //{
                //    Item item = Player.Hotbar[ItemContainer.SelectedIndex].Item;
                //    // 짓기
                //    if (!Building.GetComponent<Build>().Overlap)
                //    {
                //        if (item.ItemType == ItemType.HOUSE)
                //        {
                //            GameManager.Instance.Ui_Items.Find(o => o.Name == item.Name).Use();
                //            BuildMode = false;
                //        }
                //    }
                //    else
                //    {
                //        // 경고창 띄우기?
                //    }
                //}
                //else
                //{
                //    // 상호작용키
                //    Item item = hit.transform.gameObject.GetComponent<Item>();

                //    if (hit.transform.gameObject.GetComponent<Box>())
                //        Box = hit.transform.gameObject.GetComponent<Box>();

                //    Build build = hit.transform.gameObject.GetComponent<Build>();

                //    if (item)
                //    {
                //        Item dragged = GameManager.Instance.Ui_Items.Find(o => o.Name == item.Name);

                //        if (dragged)
                //        {
                //            Player.Backpack.AddItem(dragged, item.Count);

                //            Object.Destroy(hit.transform.gameObject);
                //            GameManager.Instance.MouseOver = false;
                //        }
                //    }
                //    else if (Box)
                //    {
                //        if (Vector2.Distance(Player.Instance.transform.position, Box.transform.position) < 3)
                //        {
                //            Box.OpenAndCloseBox(!GameManager.Instance.BoxWindow.activeSelf);
                //        }
                //    }
                //    else if (build)
                //    {
                //        if (build.Placed)
                //        {
                //            // TODO : 건물에 들어감
                //        }
                //    }
                //}
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
            ++Player.HotbarIndex;
        }
        else if (wheel < 0)
        {
            // down
            BuildMode = false;
            --Player.HotbarIndex;
        }

        if (wheel != 0)
            HotbarFrame.transform.localPosition = new Vector3(Player.Hotbar[Player.HotbarIndex].transform.localPosition.x, -465, 0);
    }

    void MouseOver()
    {
        if (!GameManager.Instance.MouseOver)
        {
            RaycastHit2D over = Physics2D.Raycast(MousePos, Vector3.forward, 1000);
            if (over)
            {
                DropItem over_item = over.transform.GetComponent<DropItem>();
                if (over_item != null)
                {
                    GameManager.Instance.SetItemInfo(over_item.Item);
                    GameManager.Instance.ItemInfoWindow.SetActive(true);
                }
            }
            else
            {
                GameManager.Instance.ItemInfoWindow.SetActive(false);
            }
        }
    }

    void KeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                // 모두
                InventoryManager.Instance.Sub(Player.Hotbar[Player.HotbarIndex], Player.Hotbar[Player.HotbarIndex].Item.Count, true, false);
            }
            // 하나
            InventoryManager.Instance.Sub(Player.Hotbar[Player.HotbarIndex], 1, true, false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            BuildMode = false;
            GameManager.Instance.MouseOver = false;
            InventoryManager.Instance.ShowInventory = !InventoryManager.Instance.ShowInventory;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameManager.Instance.MouseOver = false;
            BuildMode = !BuildMode;
        }
    }

    private void Update()
    {
        Player.transform.Translate(new Vector2(Horizontal, Vertical) * Player.Speed * Time.deltaTime);

        MouseDownLeft();
        MouseDownRight();
        MouseWheel();

        MouseOver();

        KeyBoard();
    }
}
