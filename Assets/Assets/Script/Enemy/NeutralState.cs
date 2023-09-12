using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeutralState : IBehaviourState
{
    private bool _isMoving;
    private Vector3 _destination;
    public void OnEnterState(EnemyBehaviour enemyBehaviour)
    {
        _isMoving = false;
        _destination = enemyBehaviour.transform.position;
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

        if (!_isMoving)
        {
            _isMoving = true;
            int index = UnityEngine.Random.Range(0, enemyBehaviour.Waypoints.Count);
            _destination = enemyBehaviour.Waypoints[index].position;
            enemyBehaviour.NavMeshAgent.destination = _destination;
        }
        else
        {
            if (Vector3.Distance(_destination, enemyBehaviour.transform.position) <= 0.1)
            {
                _isMoving = false;
            }
        }

        if (enemyBehaviour.Player != null)
        {
            if (Vector3.Distance(enemyBehaviour.transform.position, enemyBehaviour.Player.transform.position) < enemyBehaviour.MaxPlayerDistance)
            {
                _isMoving = false;
                enemyBehaviour.SwitchState(enemyBehaviour.ChaseState);
            }
        }

    }
}
