using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotateType
{
    X,
    Y,
    Z,
}

public class ItemRotate : MonoBehaviour
{
    public RotateType RotateType;

    private void Update()
    {
        switch (RotateType)
        {
            case RotateType.X:
                transform.Rotate(new Vector3(1, 0, 0) * 125 * Time.deltaTime);
                break;
            case RotateType.Y:
                transform.Rotate(new Vector3(0, 1, 0) * 125 * Time.deltaTime);
                break;
            case RotateType.Z:
                transform.Rotate(new Vector3(0, 0, 1) * 125 * Time.deltaTime);
                break;
        }
    }
}
