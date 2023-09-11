using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : IBehaviourState
{
    public void OnEnterState(EnemyBehaviour enemyBehaviour)
    {

    }

    public void OnExitState(EnemyBehaviour enemyBehaviour)
    {

    }

    public void OnUpdateState(EnemyBehaviour enemyBehaviour)
    {
        enemyBehaviour.NavMeshAgent.destination = enemyBehaviour.transform.position - enemyBehaviour.Player.transform.position;
        if (!enemyBehaviour.IsRetreating)
        {
            enemyBehaviour.SwitchState(enemyBehaviour.NeutralState);
        }
    }
}
