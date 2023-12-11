using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spwanbola : MonoBehaviour
{
   

    [SerializeField] public Transform[] puntos;
    [SerializeField] private GameObject bola;
    [SerializeField] private float tiempo;
    [SerializeField] private float tiemporegreso;
    public Gato existex;
    private void Start()
    {

    }
    private void Update()
    {
        if (existex.existe == false)
        {
            tiempo += (Time.deltaTime)/2;
            if (tiempo > tiemporegreso)
            {
                tiempo = 0;
                RegrsarItem();
            }
        }
    }

    private void RegrsarItem()
    {
            int posiciones = Random.Range(0, puntos.Length);
            Vector3 reaparicion = puntos[posiciones].position;
            bola.transform.position = reaparicion;
        bola.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        bola.gameObject.SetActive(true);
        existex.existe = true;

    }
}
