using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCameraMouseLook : MonoBehaviour
{
    float xRotation = 0f;
    float yRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetGameState(GameManager.GameState.StartGame))
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * GameManager.Instance.playerSettings.mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * GameManager.Instance.playerSettings.mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            yRotation += mouseX;
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }
}
