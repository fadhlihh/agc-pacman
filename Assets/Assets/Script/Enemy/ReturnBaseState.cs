using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBaseState : IBehaviourState
{
    public void OnEnterState(EnemyBehaviour enemyBehaviour)
    {
        Debug.Log("Enter Return Base");
    }

    public void OnExitState(EnemyBehaviour enemyBehaviour)
    {
        Debug.Log("Exit Return Base");
    }

    public void OnUpdateState(EnemyBehaviour enemyBehaviour)
    {
        Debug.Log("Update Return Base");
    }
}
