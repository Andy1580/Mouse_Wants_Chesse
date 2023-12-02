using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAccion_Velocidad : MonoBehaviour, IAccionItem
{
    [Range(29, 200)]
    [SerializeField] private int porcentajeVeIocidad;
    [Range(10, 60)]
    [SerializeField] private int duracion;
    private float velocidadInicial;
    public void UsarItem(Jugador jugador)
    {
        float multiplicador = 1 + ((float)porcentajeVeIocidad / 100);
        jugador.VelocidadMovimiento *= multiplicador;
        //StartCoroutine(TimerVeIocidad(jugador));
    }

    public IEnumerator TimerVeIocidad(Jugador jugador)
    {
        velocidadInicial = jugador.VelocidadMovimiento;
        float multiplicador = 1 + ((float) porcentajeVeIocidad / 100);
        jugador.VelocidadMovimiento = velocidadInicial * multiplicador;
        yield return new WaitForSeconds(duracion);
        jugador.VelocidadMovimiento = velocidadInicial;
    }

}
