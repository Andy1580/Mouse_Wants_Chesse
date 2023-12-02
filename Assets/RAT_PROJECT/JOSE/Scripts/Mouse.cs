using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mouse
{
    public static Vector3 Posicion3D
        {
        get
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit))
                return hit.point;
            return Vector3.zero;
        }
        }
}
