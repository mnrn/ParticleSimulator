using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCamera : MonoBehaviour
{
    [SerializeField] private Transform target = default;
    [SerializeField] private Vector2 rotationSpeed = default;
    private Vector2 lastMousePos = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            var x = lastMousePos.x - Input.mousePosition.x;
            var y = Input.mousePosition.y - lastMousePos.y;
            // 移動量が多い方のみを有効とします。
            if (Mathf.Abs(x) < Mathf.Abs(y))
            {
                x = 0.0f;
            } else
            {
                y = 0.0f;
            }
            var angleX = x * rotationSpeed.x;
            var angleY = y * rotationSpeed.y;

            transform.RotateAround(target.transform.position, Vector3.up, angleX);
            transform.RotateAround(target.transform.position, transform.right, angleY);

            lastMousePos = Input.mousePosition;
        }
    }
}
