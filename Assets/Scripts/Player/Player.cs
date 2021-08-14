using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float Speed { get; set; } = 3;

    public GridLayoutGroup GLG;

    public UiItem[] Hotbar = new UiItem[9];

    public PlayerState PlayerState;

    public Animator Animator;

    public SpriteRenderer sr;

    private int hotbar_index;

    public int HotbarIndex
    {
        get { return hotbar_index; }
        set
        {
            hotbar_index = value;
            hotbar_index %= 9;
            if (hotbar_index < 0) hotbar_index = 8;
        }
    }


    public TextMeshProUGUI DayText;

    private void Start()
    {
        for (int i = 0; i < 20; i++)
            InventoryManager.Instance.Add(new Item(ItemFactory.Instance.GetRandomItemData(), Random.Range(1, 6)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Tent")
        {
            CameraManager.Instance.SetBound(GameObject.Find("CamAllow").GetComponent<BoxCollider2D>());
            transform.position = InputManager.Instance.Before + new Vector3(0, -2, 0);
        }
        else
        {
            Build build = collision.GetComponent<Build>();
            if (build && build.Placed)
            {
                InputManager.Instance.Before = transform.position;

                if (build.name.Contains("텐트"))
                {
                    CameraManager.Instance.SetBound(InputManager.Instance.Tent.transform.GetChild(0).GetComponent<BoxCollider2D>());
                    transform.position = new Vector2(InputManager.Instance.Tent.transform.position.x, InputManager.Instance.Tent.transform.position.y);
                }
                else
                {
                    CameraManager.Instance.SetBound(InputManager.Instance.WasteBuilding.transform.GetChild(0).GetComponent<BoxCollider2D>());
                    transform.position = new Vector2(InputManager.Instance.WasteBuilding.transform.position.x, InputManager.Instance.WasteBuilding.transform.position.y);
                }
            }
        }
    }
}
