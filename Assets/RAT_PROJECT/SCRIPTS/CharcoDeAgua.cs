using UnityEngine;

public class CharcoDeAgua : MonoBehaviour
{
    Jugador jugador;

    private void Start()
    {
        jugador = FindAnyObjectByType<Jugador>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            jugador.VelocidadMovimiento = 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            jugador.VelocidadMovimiento = 2f;
        }
    }
}
