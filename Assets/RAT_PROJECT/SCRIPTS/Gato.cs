using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class Gato : MonoBehaviour
{
    public Transform Player;  //Asignar el personaje al que seguirán
    public float MoveSpeed = 4; //Establecer velocidad de persecución
    public float MaxDist = 20; //Establecer distancia máxima a la que lo seguirá
    public float MinDist = 1;//Establecer distancia mínima a la que lo seguirá

    //var idle:AnimationClip; //Animación en estado de reposo
    //var run:AnimationClip; //Animación de correr o perseguir


    public void Start()
    {

    }

    public void Update()
    {
        var EnemyPos = transform.position;
        var PlayerPos = Player.position;
        var distancia = Vector3.Distance(EnemyPos, PlayerPos);

            if (distancia >= MinDist && distancia <= MaxDist)
            {
                var targetPos = new Vector3(Player.position.x,
                                                this.transform.position.y,
                                                Player.position.z);
                transform.LookAt(targetPos);
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                //animation.CrossFade(run.name, 1);
                //for (var state ) //: AnimationState in animation
                //{
                //    state.speed = 2;
                //}
                //}
                //else
                //{
                //    //animation.CrossFade(idle.name, 1);
                //    for (var state /*: AnimationState in animation*/)
                //    {
                //        state.speed = 1;
                //    }
            }
        
    }

    public Transform objetivo;
    private NavMeshAgent agent;



}
