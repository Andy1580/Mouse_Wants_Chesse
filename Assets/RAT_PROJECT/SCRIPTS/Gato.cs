using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;


public class Gato : MonoBehaviour
{
    public Transform Player;  //Asignar el personaje al que seguirán
    public GatoVision muerte;
    public float MaxDist; //Establecer distancia en la que puede escuchar al personaje
    public LayerMask capPlayer; // Layer que verificara
    public bool alerta; // si/no de que escucho al personaje

    public bool bola; // la bola esta en su rango
    public LayerMask capBola; // Layer que verificara
    public Transform Bola;
    public NavMeshAgent agent;
    public Animator gatoanimator;

    ////var idle:AnimationClip; //Animación en estado de reposo
    ////var run:AnimationClip; //Animación de correr o perseguir


    public void Start()
    {
        isAlive = true;
        col = GetComponent<Collider>();
    }

    public void Update()
    {
   
            Deteccion();
        

            


        //var EnemyPos = transform.position;
        //var PlayerPos = Player.position;
        //var distancia = Vector3.Distance(EnemyPos, PlayerPos);

        //    if (distancia >= MinDist && distancia <= MaxDist)
        //    {
        //        var targetPos = new Vector3(Player.position.x,
        //                                        this.transform.position.y,
        //                                        Player.position.z);
        //        transform.LookAt(targetPos);
        //        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        //        //animation.CrossFade(run.name, 1);
        //        //for (var state ) //: AnimationState in animation
        //        //{
        //        //    state.speed = 2;
        //        //}
        //        //}
        //        //else
        //        //{
        //        //    //animation.CrossFade(idle.name, 1);
        //        //    for (var state /*: AnimationState in animation*/)
        //        //    {
        //        //        state.speed = 1;
        //        //    }
        //    }

    }

    public void Deteccion()
    {
        alerta = Physics.CheckSphere(transform.position, MaxDist, capPlayer);
        bola = Physics.CheckSphere(transform.position, MaxDist, capBola);

        if (alerta == true && bola == false)
        {
            Vector3 posPlayer = new Vector3(Player.position.x, Player.position.y, Player.position.z);
            transform.LookAt(posPlayer);
            agent.SetDestination(Player.transform.position);
            gatoanimator.SetBool("IsRunning", true);
            //transform.position = Vector3.MoveTowards(transform.position, posPlayer, MoveSpeed * Time.deltaTime);
        }
        if (alerta == true && bola == false && muerte.RatDead == true)
        {
            Vector3 posPlayer = new Vector3(Player.position.x, Player.position.y, Player.position.z);
            transform.LookAt(posPlayer);
            gatoanimator.SetBool("IsRunning", false);
            //transform.position = Vector3.MoveTowards(transform.position, posPlayer, MoveSpeed * Time.deltaTime);
        }

        if (alerta == true && bola == true)
        {
            Vector3 posBola = new Vector3(Bola.position.x, Bola.position.y, Bola.position.z);
            transform.LookAt(posBola);
            //transform.position = Vector3.MoveTowards(transform.position, posPlayer, MoveSpeed * Time.deltaTime);
            return;
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isAlive == true && muerte.RatDead == true)
            {
                //Destroy(playerPrefab);
                playerPrefab.SetActive(false);
                Time.timeScale = 0f;
                gatoanimator.SetBool("IsRunning", false);
            }
        }
    }
}
