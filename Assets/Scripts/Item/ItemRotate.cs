using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * 125 * Time.deltaTime);
    }
}
