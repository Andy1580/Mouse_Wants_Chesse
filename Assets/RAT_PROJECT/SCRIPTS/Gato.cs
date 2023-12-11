using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using Unity.VisualScripting;


public class Gato : MonoBehaviour
{
    public Transform Player;  //Asignar el personaje al que seguirán
    //public GatoVision muerte;
    public float MaxDist; //Establecer distancia en la que puede escuchar al personaje
    public LayerMask capPlayer; // Layer que verificara
    public bool alerta; // si/no de que escucho al personaje

    public bool bola; // la bola esta en su rango
    public LayerMask capBola; // Layer que verificara
    public Transform Bola;
    public NavMeshAgent agent;
    public Animator gatoanimator;
    public Transform home;
    public GameObject estambre;
    public bool existe;

    public bool RatDead;

    //public Transform detenerse;

    public void Start()
    {
        existe = true;
        bola = false;
        isAlive = true;
        col = GetComponent<Collider>();
        GetComponent<Transform>().position = home.position;
    }

    public void Update()
    {
        Deteccion();
        // Obtén la dirección de movimiento del agente
        Vector3 direccionMovimiento = agent.velocity.normalized;

        // Calcula la rotación basada en la dirección de movimiento
        Quaternion rotacionDeseada = Quaternion.LookRotation(direccionMovimiento);

        // Aplica la rotación al objeto
        agent.transform.rotation = rotacionDeseada;

        if (agent.isOnOffMeshLink)
        {
            gatoanimator.SetBool("IsJumping2", true);
        }
        else
        {
            gatoanimator.SetBool("IsJumping2", false);
        }
    }

    public void Deteccion()
    {
        alerta = Physics.CheckSphere(transform.position, MaxDist, capPlayer);
        bola = Physics.CheckSphere(transform.position, MaxDist, capBola);

        if (alerta == true)
        {
            Vector3 posPlayer = new Vector3(Player.position.x, Player.position.y, Player.position.z);
            transform.LookAt(posPlayer);
            agent.SetDestination(Player.transform.position);
            gatoanimator.SetBool("IsRunning", true);
            //transform.position = Vector3.MoveTowards(transform.position, posPlayer, MoveSpeed * Time.deltaTime);
        }
        else if (alerta == false)
        {
            //transform.LookAt(home.transform.position);
            agent.SetDestination(home.transform.position);
            if (agent.transform.position == home.transform.position)
            {
                gatoanimator.SetBool("IsRunning", false);
            }
        }
        if (alerta == true && bola == true)
        {
            Vector3 posBola = new Vector3(Bola.position.x, Bola.position.y, Bola.position.z);
            transform.LookAt(posBola);
            Debug.Log("¡Bola de estambre detectado!");
            agent.SetDestination(Bola.transform.position);
            gatoanimator.SetBool("IsRunning", true);
        }
        if (alerta == false && bola == true)
        {
            Vector3 posBola = new Vector3(Bola.position.x, Bola.position.y, Bola.position.z);
            transform.LookAt(posBola);
            Debug.Log("¡Bola de estambre detectado!");
            agent.SetDestination(Bola.transform.position);
            gatoanimator.SetBool("IsRunning", true);
        }
        else if (alerta == false && bola == false && agent.transform.position != home.transform.position)
        {
            gatoanimator.SetBool("IsRunning", true);
            //transform.LookAt(home.transform.position);
            agent.SetDestination(home.transform.position);
            if (agent.transform.position == home.transform.position)
            {
                gatoanimator.SetBool("IsRunning", false);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, MaxDist);
        //Gizmos.DrawWireSphere(transform.position, viewDistance);
    }
    [SerializeField] private bool isAlive;
    [SerializeField] private GameObject playerPrefab;
    Collider col;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isAlive == true)
            {
                RatDead = true;
                //Destroy(playerPrefab);
                playerPrefab.SetActive(false);
                Time.timeScale = 0f;
                gatoanimator.SetBool("IsRunning", false);
            }
        }
        if (other.gameObject.tag == "Bola")
        {
            Debug.Log("bolita");
            //Destroy(playerPrefab);
            estambre.SetActive(false);
            existe = false;
            //Time.timeScale = 0f;
            gatoanimator.SetBool("IsRunning", false);
            bola = false;

        }
    }





}
