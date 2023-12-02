using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemigo : Vida
{
    #region Core
    private NavMeshAgent nav;

    protected new void Awake()
    {
        base.Awake();
        nav = GetComponent<NavMeshAgent>();
    }

    public override float VelocidadMovimiento
    {
        set
        {
            base.VelocidadMovimiento = value;
            nav.speed = value;
        }
    }


    //public TargetScanner playerScanner;

    //public void FindTarget()
    //{
    //    //we ignore height difference if the target was already seen
    //    PlayerController target = playerScanner.Detect(transform, m_Target == null);

    //    if (m_Target == null)
    //    {
    //        //we just saw the player for the first time, pick an empty spot to target around them
    //        if (target != null)
    //        {
    //            m_Controller.animator.SetTrigger(hashSpotted);
    //            m_Target = target;
    //            TargetDistributor distributor = target.GetComponentInChildren<TargetDistributor>();
    //            if (distributor != null)
    //                m_FollowerInstance = distributor.RegisterNewFollower();
    //        }
    //    }
    //    else
    //    {
    //        //we lost the target. But chomper have a special behaviour : they only loose the player scent if they move past their detection range
    //        //and they didn't see the player for a given time. Not if they move out of their detectionAngle. So we check that this is the case before removing the target
    //        if (target == null)
    //        {
    //            m_TimerSinceLostTarget += Time.deltaTime;

    //            if (m_TimerSinceLostTarget >= timeToStopPursuit)
    //            {
    //                Vector3 toTarget = m_Target.transform.position - transform.position;

    //                if (toTarget.sqrMagnitude > playerScanner.detectionRadius * playerScanner.detectionRadius)
    //                {
    //                    if (m_FollowerInstance != null)
    //                        m_FollowerInstance.distributor.UnregisterFollower(m_FollowerInstance);

    //                    //the target move out of range, reset the target
    //                    m_Target = null;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (target != m_Target)
    //            {
    //                if (m_FollowerInstance != null)
    //                    m_FollowerInstance.distributor.UnregisterFollower(m_FollowerInstance);

    //                m_Target = target;

    //                TargetDistributor distributor = target.GetComponentInChildren<TargetDistributor>();
    //                if (distributor != null)
    //                    m_FollowerInstance = distributor.RegisterNewFollower();
    //            }

    //            m_TimerSinceLostTarget = 0.0f;
    //        }
    //    }
    //}

    //public void StartPursuit()
    //{
    //    if (m_FollowerInstance != null)
    //    {
    //        m_FollowerInstance.requireSlot = true;
    //        RequestTargetPosition();
    //    }

    //    m_Controller.animator.SetBool(hashInPursuit, true);
    //}

    //public void StopPursuit()
    //{
    //    if (m_FollowerInstance != null)
    //    {
    //        m_FollowerInstance.requireSlot = false;
    //    }

    //    m_Controller.animator.SetBool(hashInPursuit, false);
    //}

    //public void RequestTargetPosition()
    //{
    //    Vector3 fromTarget = transform.position - m_Target.transform.position;
    //    fromTarget.y = 0;

    //    m_FollowerInstance.requiredPoint = m_Target.transform.position + fromTarget.normalized * attackDistance * 0.9f;
    //}

    //public void WalkBackToBase()
    //{
    //    if (m_FollowerInstance != null)
    //        m_FollowerInstance.distributor.UnregisterFollower(m_FollowerInstance);
    //    m_Target = null;
    //    StopPursuit();
    //    m_Controller.SetTarget(originalPosition);
    //    m_Controller.SetFollowNavmeshAgent(true);
    //}

    //public void TriggerAttack()
    //{
    //    m_Controller.animator.SetTrigger(hashAttack);
    //}

    //public void AttackBegin()
    //{
    //    meleeWeapon.BeginAttack(false);
    //}

    //public void AttackEnd()
    //{
    //    meleeWeapon.EndAttack();
    //}

    //public void OnReceiveMessage(Message.MessageType type, object sender, object msg)
    //{
    //    switch (type)
    //    {
    //        case Message.MessageType.DEAD:
    //            Death((Damageable.DamageMessage)msg);
    //            break;
    //        case Message.MessageType.DAMAGED:
    //            ApplyDamage((Damageable.DamageMessage)msg);
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
#endregion Core


