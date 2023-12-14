using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuesos : MonoBehaviour
{
    [SerializeField] public Transform[] puntos;
    [SerializeField] public Transform[] puntos1;
    [SerializeField] public Transform[] puntos2;
    [SerializeField] private GameObject queso;
    [SerializeField] private GameObject queso1;
    [SerializeField] private GameObject queso2;

    private void Start()
    {
        int posiciones = Random.Range(0, puntos.Length);
        Vector3 reaparicion = puntos[posiciones].position;
        queso.transform.position = reaparicion;
        queso.gameObject.SetActive(true);

        int posiciones1 = Random.Range(0, puntos1.Length);
        Vector3 reaparicion1 = puntos1[posiciones1].position;
        queso1.transform.position = reaparicion1;
        queso1.gameObject.SetActive(true);

        int posiciones2 = Random.Range(0, puntos2.Length);
        Vector3 reaparicion2 = puntos2[posiciones2].position;
        queso2.transform.position = reaparicion2;
        queso2.gameObject.SetActive(true);
    }
}

