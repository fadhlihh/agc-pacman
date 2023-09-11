using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBaseState : IBehaviourState
{
    public void OnEnterState(EnemyBehaviour enemyBehaviour)
    {
    }

    public void OnExitState(EnemyBehaviour enemyBehaviour)
    {
    }

    public void OnUpdateState(EnemyBehaviour enemyBehaviour)
    {
        if (Vector3.Distance(enemyBehaviour.SpawnPoint.position, enemyBehaviour.transform.position) > 1)
        {
            enemyBehaviour.NavMeshAgent.destination = enemyBehaviour.SpawnPoint.position;
        }
        else
        {
            enemyBehaviour.Return();
        }
    }
}
