using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegStamina : MonoBehaviour
{

    public float Recuestam;
    public Image StaminaBar;

    private void OnTriggerStay(Collider other)
    {
        
        if (Input.GetKey(KeyCode.Q))
        {

            other.GetComponent<Jugador>().Estamina();
        }
        
    }

}
