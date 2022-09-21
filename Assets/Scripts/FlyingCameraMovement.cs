using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCameraMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        transform.position += move * 0.5f;
    }
}
