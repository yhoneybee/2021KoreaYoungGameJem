using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; } = null;

    public GameObject target;
    public float cameraSpeed;
    private Vector3 targetPosition;

    private Camera theCamera;

    public BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float halfHeight;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clapmedY = Mathf.Clamp(transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            transform.position = new Vector3(clampedX, clapmedY, transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D box2D)
    {
        bound = box2D;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
