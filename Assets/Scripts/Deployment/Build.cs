using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public SpriteRenderer sr;

    [SerializeField]
    bool placed = false;
    public bool Placed
    {
        get { return placed; }
        set
        {
            placed = value;
            if (placed)
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    [SerializeField]
    private bool overlap;

    public bool Overlap
    {
        get { return overlap; }
        set
        {
            if (!Placed)
            {
                overlap = value;

                if (overlap)
                    GetComponent<SpriteRenderer>().color = new Color(1, 0.3f, 0.3f, 0.5f);
                else
                    GetComponent<SpriteRenderer>().color = new Color(0.3f, 1, 0.3f, 1);
            }
        }
    }

    private void Start()
    {
        Overlap = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != gameObject && collision.gameObject.GetComponent<Build>())
            Overlap = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != gameObject && collision.gameObject.GetComponent<Build>())
            Overlap = false;
    }
}
