using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public ItemData ItemData;
    public SpriteRenderer Rotate;

    private void Start()
    {
        Rotate.sprite = ItemData.Ingame;
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * 3 * Time.deltaTime);
    }
}
