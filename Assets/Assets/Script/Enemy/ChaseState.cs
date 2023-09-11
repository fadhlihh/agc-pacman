using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IBehaviourState
{
    public void OnEnterState(EnemyBehaviour enemyBehaviour)
    {

    }

    public void OnExitState(EnemyBehaviour enemyBehaviour)
    {

    }

    public void OnUpdateState(EnemyBehaviour enemyBehaviour)
    {
        if (enemyBehaviour.IsRetreating)
        {
            enemyBehaviour.SwitchState(enemyBehaviour.RetreatState);
        }

        enemyBehaviour.NavMeshAgent.destination = enemyBehaviour.Player.transform.position;
        Debug.LogWarning(enemyBehaviour.gameObject.name + " Chase Player");

        if (Vector3.Distance(enemyBehaviour.transform.position, enemyBehaviour.Player.transform.position) > enemyBehaviour.MaxPlayerDistance)
        {
            Debug.LogWarning("Lost Player");
            enemyBehaviour.SwitchState(enemyBehaviour.NeutralState);
        }
    }
}
