using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; } = null;

    public float Horizontal
    {
        get
        {
            Player.Animator.SetFloat("LeftAndRight", Input.GetAxis("Horizontal"));
            return Input.GetAxis("Horizontal");
        }
    }
    public float Vertical
    {
        get
        {
            Player.Animator.SetFloat("UpAndDown", Input.GetAxis("Vertical"));
            return Input.GetAxis("Vertical");
        }
    }
    public Vector2 MousePos => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private bool build_mode;

    public Build Build;
    public Build Building { get; set; }

    public GameObject HotbarFrame;
    Vector2 Dir { get; set; }
    float Angle { get; set; }
    public Quaternion Quaternion { get; set; }

    public GameObject Tent;
    public GameObject WasteBuilding;

    public Vector3 Before;

    public bool BuildMode
    {
        get { return build_mode; }
        set
        {
            build_mode = value;

            if (build_mode)
            {
                var item = GameManager.Instance.Player.Hotbar[GameManager.Instance.Player.HotbarIndex].Item;

                if (item != null && item.Count > 0 && item.Data.ItemType == ItemType.HOUSE)
                {
                    Building = Instantiate(Build);
                    Building.name = item.Data.Name;
                    Building.sr.sprite = item.Data.Ingame;
                }
            }
            else
            {
                if (Building)
                    Destroy(Building.gameObject);
            }
        }
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

    void SetAngle()
    {
        Dir = MousePos - new Vector2(Player.transform.position.x, Player.transform.position.y);

        Angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        Quaternion = Quaternion.AngleAxis(Angle - 90, Vector3.forward);
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
                    Build build = hit.transform.GetComponent<Build>();
                    if (build)
                    {
                        InventoryManager.Instance.Add(new Item(ItemFactory.Instance.GetRandomItemData(), Random.Range(1, 4)));
                        Destroy(build.gameObject);
                    }
                }
            }
            else
            {
                // 사용
                Item item = Player.Hotbar[Player.HotbarIndex].Item;

                if (!InventoryManager.Instance.ShowBox && !InventoryManager.Instance.ShowInventory)
                {
                    if (item != null && item.Data.ItemType != ItemType.HOUSE)
                        item.Use();
                }
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
                    var item = Player.Hotbar[Player.HotbarIndex].Item;
                    if (!Building.Overlap)
                    {
                        if (item.Data.ItemType == ItemType.HOUSE)
                        {
                            item.Use();
                            BuildMode = false;
                        }
                    }
                }
                else
                {
                    // 상호작용
                    DropItem item = hit.transform.GetComponent<DropItem>();
                    Box box = hit.transform.GetComponent<Box>();
                    Collection collection = hit.transform.GetComponent<Collection>();

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
                    else if (collection)
                    {
                        collection.Collect();
                    }
                }
            }
            else
            {
                if (InventoryManager.Instance.ShowBox)
                    InventoryManager.Instance.ShowBox = false;
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
            HotbarFrame.GetComponent<RectTransform>().anchoredPosition = new Vector3(Player.Hotbar[Player.HotbarIndex].transform.localPosition.x, 12.5f, 0);
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

    void TempBuild()
    {
        if (BuildMode)
        {
            Item select = Player.Hotbar[Player.HotbarIndex].Item;
            if (select != null && select.Data.ItemType == ItemType.HOUSE)
            {
                Building.transform.position = new Vector3(MousePos.x, MousePos.y, 102);
            }
        }
    }

    private void Update()
    {
        Player.transform.Translate(new Vector2(Horizontal, Vertical) * Player.Speed * Time.deltaTime);

        SetAngle();

        MouseDownLeft();
        MouseDownRight();
        MouseWheel();

        MouseOver();

        KeyBoard();

        TempBuild();
    }
}
