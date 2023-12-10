using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public GameManager manager;
    public Win gane;
    public RumbaFunctions muerte;
    public Pause pausess;


    public Transform orientation;
    public float xRotation;
    public float yRotation;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(manager.manager == true && gane.win == false && muerte.IsDead == false && pausess.pauseCanvas == false)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") + Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, 0, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
            //orientation.rotation = Quaternion.Euler(xRotation, 0, 0);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if(gane.win == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if(muerte.IsDead == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (pausess.pauseCanvas == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

}
