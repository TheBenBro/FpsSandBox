using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    float xRotation = 0f;
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
            float mouseX = Input.GetAxisRaw("Mouse X") * GameManager.Instance.playerSettings.GetMouseSensitivity() * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * GameManager.Instance.playerSettings.GetMouseSensitivity() * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -70f, 70f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
