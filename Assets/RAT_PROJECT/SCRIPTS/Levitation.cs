using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Levitation : MonoBehaviour
{
  
    public float amplitude = 0.5f;
    public float frequency = 1f;
     [SerializeField] public int speed = 200;

    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    void Start()
    {
        
        posOffset = transform.position;
    }

    void Update()
    {
        Flotar();
    }

    public void Flotar()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.Rotate(Vector3.up, speed * Time.deltaTime);

        transform.position = tempPos;
    }
}
