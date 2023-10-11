using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalar : MonoBehaviour
{
    bool inside = false;
    public float gravity = 2000.0f;
    private Vector3 moveDirection = Vector3.zero;


    void Start()
    {

        inside = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            moveDirection.y += gravity * Time.deltaTime;
            inside = !inside;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {

            inside = !inside;
        }
    }

    void Update()
    {
        moveDirection.y -= gravity * Time.deltaTime;


    }


}
