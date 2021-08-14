using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0.5f, 0) * 3 * Time.deltaTime);
    }
}
