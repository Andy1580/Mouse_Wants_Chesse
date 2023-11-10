using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class Gato : MonoBehaviour
{
    public Transform Player;  //Asignar el personaje al que seguirán
    public float MaxDist; //Establecer distancia en la que puede escuchar al personaje
    public LayerMask capPlayer; // Layer que verificara
    public bool alerta; // si/no de que escucho al personaje

    ////var idle:AnimationClip; //Animación en estado de reposo
    ////var run:AnimationClip; //Animación de correr o perseguir

    public void Start()
    {

    }

    public void Update()
    {
        alerta = Physics.CheckSphere(transform.position, MaxDist, capPlayer);


        if (alerta == true)
        {
            Vector3 posPlayer = new Vector3(Player.position.x, transform.position.y, Player.position.z);
            transform.LookAt(posPlayer);
            //transform.position = Vector3.MoveTowards(transform.position, posPlayer, MoveSpeed * Time.deltaTime);
        }


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

    private NavMeshAgent agent;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, MaxDist);
        //Gizmos.DrawWireSphere(transform.position, viewDistance);
    }

}
