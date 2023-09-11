using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : IBehaviourState
{
    public void OnEnterState(EnemyBehaviour enemyBehaviour)
    {
        // Debug.Log("Enter Retreat");
    }

    public void OnExitState(EnemyBehaviour enemyBehaviour)
    {
        // Debug.Log("Exit Retreat");
    }

    public void OnUpdateState(EnemyBehaviour enemyBehaviour)
    {
        // Debug.Log("Update Retreat");
        enemyBehaviour.NavMeshAgent.destination = enemyBehaviour.transform.position - enemyBehaviour.Player.transform.position;
        if (!enemyBehaviour.IsRetreating)
        {
            enemyBehaviour.SwitchState(enemyBehaviour.NeutralState);
        }
    }
}
