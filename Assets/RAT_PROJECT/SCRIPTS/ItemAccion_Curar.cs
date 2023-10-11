using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAccion_Curar : MonoBehaviour , IAccionItem
{
    [SerializeField] private int curacion = 50;
    public void UsarItem (Jugador jugador)
    {
        //jugador.vida += curacion;
    }
}
