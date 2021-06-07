using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform player;
    public Camera playerCam;

    public float mouseSensitivity = 10f;
    public float lookClamp = 90f;

    private float x = 0f;
    private float y = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle mouselook
        x += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        y += Input.GetAxis("Mouse X") * mouseSensitivity;

        x = Mathf.Clamp(x, -lookClamp, lookClamp);

        playerCam.transform.localRotation = Quaternion.Euler(x, 0, 0);
        player.transform.localRotation = Quaternion.Euler(0, y, 0);

        // Disable/re-enable mouselook when pressing escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cusor.lockState = CusorLockMode.Locked ? CusorLockMode.None : CusorLockMode.Locked;
        }
    }
}
