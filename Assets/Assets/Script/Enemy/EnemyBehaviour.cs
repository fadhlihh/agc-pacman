using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    public Player Player;
    [SerializeField]
    public float MaxPlayerDistance;
    public NavMeshAgent NavMeshAgent;
    public bool IsRetreating;
    public bool IsSpawned;
    public List<Transform> Waypoints = new List<Transform>();

    private WaitingState _waitingState = new WaitingState();
    private NeutralState _neutralState = new NeutralState();
    private ChaseState _chaseState = new ChaseState();
    private RetreatState _retreatState = new RetreatState();
    private ReturnBaseState _returnBaseState = new ReturnBaseState();
    private IBehaviourState _currentState;
    private Animator _animator;

    public WaitingState WaitingState { get { return _waitingState; } }
    public NeutralState NeutralState { get { return _neutralState; } }
    public ChaseState ChaseState { get { return _chaseState; } }
    public RetreatState RetreatState { get { return _retreatState; } }
    public ReturnBaseState ReturnBaseState { get { return _returnBaseState; } }

    public void SwitchState(IBehaviourState state)
    {
        _currentState.OnExitState(this);
        _currentState = state;
        _currentState.OnEnterState(this);
    }

    public void ReturnToBase()
    {
        Debug.Log("Enemy Dead");
        SwitchState(_returnBaseState);
    }

    public void Retreat()
    {
        IsRetreating = true;
    }

    public void StopRetreat()
    {
        IsRetreating = false;
    }

    public void SetNavMeshStatus(bool isActive)
    {
        NavMeshAgent.enabled = isActive;
    }

    public void AddWaypoint(Transform waypoint)
    {
        Waypoints.Add(waypoint);
    }

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _currentState = _waitingState;
        _currentState.OnEnterState(this);
    }

    private void Start()
    {
        Player = GameObject.FindAnyObjectByType<Player>();
        if (Player != null)
        {
            Player.OnPowerUpStart += Retreat;
            Player.OnPowerUpGone += StopRetreat;
        }
    }

    private void Update()
    {
        Debug.Log(gameObject.name + ": " + _currentState);
        _currentState.OnUpdateState(this);
        _animator.SetFloat("Speed", NavMeshAgent.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !IsRetreating)
        {
            other.gameObject.GetComponent<Player>().Dead();
        }
    }
}
