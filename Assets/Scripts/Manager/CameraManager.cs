using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(target.transform.position.x, target.transform.position.y, Camera.main.transform.position.z), Time.deltaTime * 10);
    }
}
