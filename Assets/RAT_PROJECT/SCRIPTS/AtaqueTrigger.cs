using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueTrigger : MonoBehaviour
{
    private Jugador jugador;
    private void Awake()
    {
        jugador = transform.parent.GetComponent<Jugador>();
    }

private void OnTriggerEnter(Collider other)
    {
        // Lager 6: Enemigos
        if (other.gameObject.layer == 6)
        jugador.OnAtaqueTriggerEnter(other.GetComponent<Enemigo>());
    }

}
