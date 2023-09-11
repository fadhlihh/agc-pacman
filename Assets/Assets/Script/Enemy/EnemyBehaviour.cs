using System;
using System.Collections;
using System.Collections.Generic;
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
    public Transform SpawnPoint;
    public Transform ReturnPoint;

    [SerializeField]
    private GameObject _eyes;
    [SerializeField]
    private GameObject _mesh;

    private WaitingState _waitingState = new WaitingState();
    private NeutralState _neutralState = new NeutralState();
    private ChaseState _chaseState = new ChaseState();
    private RetreatState _retreatState = new RetreatState();
    private ReturnBaseState _returnBaseState = new ReturnBaseState();
    private IBehaviourState _currentState;
    private Animator _animator;
    private Action<EnemyBehaviour> _onReturn;

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
        _mesh.SetActive(false);
        _eyes.SetActive(true);
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

    public void AddWaypoint(Transform waypoint)
    {
        Waypoints.Add(waypoint);
    }

    public void Reset()
    {
        _mesh.SetActive(true);
        _eyes.SetActive(false);
        NavMeshAgent.enabled = false;
        transform.rotation = Quaternion.identity;
        transform.position = ReturnPoint.position;
        SwitchState(WaitingState);
    }

    public void Return()
    {
        Reset();
        _onReturn(this);
    }

    public void InitEnemy(Transform spawnPoint, Transform returnPoint, Action<EnemyBehaviour> onReturn)
    {
        SpawnPoint = spawnPoint;
        ReturnPoint = returnPoint;
        _onReturn += onReturn;
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
