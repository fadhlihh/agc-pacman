using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IBehaviourState
{
    public void OnEnterState(EnemyBehaviour enemyBehaviour)
    {
        // Debug.Log("Enter Chase");
    }

    public void OnExitState(EnemyBehaviour enemyBehaviour)
    {
        // Debug.Log("Exit Chase");
    }

    public void OnUpdateState(EnemyBehaviour enemyBehaviour)
    {
        if (enemyBehaviour.IsRetreating)
        {
            enemyBehaviour.SwitchState(enemyBehaviour.RetreatState);
        }

        enemyBehaviour.NavMeshAgent.destination = enemyBehaviour.Player.transform.position;

        if (Vector3.Distance(enemyBehaviour.transform.position, enemyBehaviour.Player.transform.position) > enemyBehaviour.MaxPlayerDistance)
        {
            enemyBehaviour.SwitchState(enemyBehaviour.NeutralState);
        }
    }
}
