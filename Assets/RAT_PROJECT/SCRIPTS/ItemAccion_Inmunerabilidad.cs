using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAccion_Inmunerabilidad : MonoBehaviour,IAccionItem
{
    [SerializeField] private float duracion = 5;

    public void UsarItem(Jugador jugador)
    {
        jugador.Inmunerabilidad(duracion);
    }
}
