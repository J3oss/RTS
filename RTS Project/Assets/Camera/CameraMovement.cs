using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float mouseCameraMoveSpeed = 2;
    [SerializeField] float keyboardCameraMoveSpeed = 2;

    [SerializeField] int xMouseMoveTrigger = 20;
    [SerializeField] int yMouseMoveTrigger = 20;

    private float currentCameraX = 0, currentCameraY = 20, currentCameraZ = 0;
    private Vector3 moveDirection;

    ///////////////////////////////////////////////////////////////////
    //TODO move to a function so that if player maximizes or minimizes it update
    private int screenWidth = Screen.width;
    private int screenHeight = Screen.height;

    private int screenWidthMidPoint = Screen.width / 2;
    private int screenHeightMidPoint = Screen.height / 2;
    ////////////////////////////////////////////////////////////////////

    void Start()
    {
        currentCameraX = gameObject.transform.position.x;
        currentCameraZ = gameObject.transform.position.z;
    }

    private void MouseMovement()
    {
        //left and right
        if (Input.mousePosition.x <= xMouseMoveTrigger)
        {
            //moveDirection = new Vector3(1, 0, ((Input.mousePosition.y - screenHeightMidPoint) / screenHeightMidPoint) * 1).normalized;
            moveDirection = new Vector3(1, 0, 0);

            currentCameraX = currentCameraX - moveDirection.x*mouseCameraMoveSpeed;
            currentCameraZ = currentCameraZ + moveDirection.z * mouseCameraMoveSpeed;
        }
        else if (Input.mousePosition.x >= screenWidth - xMouseMoveTrigger)
        {
            //moveDirection = new Vector3(1, 0, ((Input.mousePosition.y - screenHeightMidPoint) / screenHeightMidPoint) * 1).normalized;
            moveDirection = new Vector3(1, 0, 0);

            currentCameraX = currentCameraX + moveDirection.x * mouseCameraMoveSpeed;
            currentCameraZ = currentCameraZ + moveDirection.z * mouseCameraMoveSpeed;
        }

        //up  and down
        if (Input.mousePosition.y >= screenHeight - yMouseMoveTrigger)
        {
            //moveDirection = new Vector3(((Input.mousePosition.x - screenWidthMidPoint) / screenWidthMidPoint) * 1, 0, 1).normalized;
            moveDirection = new Vector3(0, 0, 1);

            currentCameraX = currentCameraX + moveDirection.x * mouseCameraMoveSpeed;
            currentCameraZ = currentCameraZ + moveDirection.z * mouseCameraMoveSpeed;
        }
        else if (Input.mousePosition.y <= yMouseMoveTrigger)
        {
            //moveDirection = new Vector3(((Input.mousePosition.x - screenWidthMidPoint) / screenWidthMidPoint) * 1, 0, 1).normalized;
            moveDirection = new Vector3(0, 0, 1);

            currentCameraX = currentCameraX + moveDirection.x * mouseCameraMoveSpeed;
            currentCameraZ = currentCameraZ - moveDirection.z * mouseCameraMoveSpeed;
        }
    }

    private void KeyboardMovement()
    {
        if (Input.GetKey(KeyCode.D))
            currentCameraX = currentCameraX + keyboardCameraMoveSpeed;
        if (Input.GetKey(KeyCode.A))
            currentCameraX = currentCameraX - keyboardCameraMoveSpeed;

        if (Input.GetKey(KeyCode.W))
            currentCameraZ = currentCameraZ + keyboardCameraMoveSpeed;
        if (Input.GetKey(KeyCode.S))
            currentCameraZ = currentCameraZ - keyboardCameraMoveSpeed;
    }

    void Update()
    {
        KeyboardMovement();
        MouseMovement();

        gameObject.transform.position = new Vector3(currentCameraX, currentCameraY, currentCameraZ);
    }


}
