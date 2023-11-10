using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    [SerializeField] private Transform[] pointsToMove;
    [SerializeField] private float speedMovement;
    [SerializeField] private float maxDistance;

    private int nextStep = 0;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointsToMove[nextStep].position, speedMovement * Time.deltaTime);

        if (Vector3.Distance(transform.position, pointsToMove[nextStep].position) < maxDistance)
        {
            nextStep += 1;
            if (nextStep >= pointsToMove.Length)
            {
                nextStep = 0;
            }
        }
    }
}
