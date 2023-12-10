using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spwanbola : MonoBehaviour
{
    
    private float minX, minZ, maxX, maxZ,minY,maxY;

    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject bola;
    [SerializeField] private float tiempo;
    [SerializeField] private float tiemporegreso;
    private void Start()
    {

        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxZ = puntos.Max(punto => punto.position.z);
        minZ = puntos.Min(punto => punto.position.z);
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);

    }
    private void Update()
    {

        tiempo += Time.deltaTime;
        if(tiempo > tiemporegreso)
        {
            tiempo = 0;
            RegrsarItem();
        }


           
    }

    private void RegrsarItem()
    {

            Vector3 posicionaleatoria = new Vector3(Random.Range(minZ, maxZ), Random.Range(minX, maxX), Random.Range(minY, maxY));
            Instantiate(bola,posicionaleatoria, Quaternion.identity);

    }
}
