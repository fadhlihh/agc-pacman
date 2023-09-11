using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IBehaviourState
{
    public void OnEnterState(EnemyBehaviour enemyBehaviour);
    public void OnUpdateState(EnemyBehaviour enemyBehaviour);
    public void OnExitState(EnemyBehaviour enemyBehaviour);
}
