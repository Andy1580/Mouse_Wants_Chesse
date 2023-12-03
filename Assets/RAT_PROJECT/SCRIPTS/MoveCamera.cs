using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;


        //con los nuevos modelos
        //transform.rotation = cameraPosition.rotation; transform.localScale = cameraPosition.localScale;
            //Quaternion.Euler(0, 90, -180);


    }

}
