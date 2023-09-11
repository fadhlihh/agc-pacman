using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private int _enemyCount;
    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private float _releaseInterval;
    [SerializeField]
    private EnemyBehaviour _enemyPrefabs;
    [SerializeField]
    private List<Transform> _waypoints = new List<Transform>();
    [SerializeField]
    private List<Transform> _returnPoint = new List<Transform>();

    private Queue<EnemyBehaviour> _enemyQueue = new Queue<EnemyBehaviour>();
    private List<EnemyBehaviour> _enemyList = new List<EnemyBehaviour>();
    private bool _isSpawning = false;
    private Coroutine _enemyRelease;

    public void ReturnAllEnemy()
    {
        if (_enemyRelease != null)
        {
            StopCoroutine(_enemyRelease);
        }
        _isSpawning = false;
        _enemyQueue.Clear();
        foreach (EnemyBehaviour enemy in _enemyList)
        {
            _enemyQueue.Enqueue(enemy);
            enemy.Reset();
        }
        StartSpawnEnemy();
    }

    public void ReturnEnemy(EnemyBehaviour enemy)
    {
        _enemyQueue.Enqueue(enemy);
        if (_enemyQueue.Count <= 1)
        {
            StartSpawnEnemy();
        }
    }

    private void Awake()
    {
        InitEnemy();
    }

    private void Start()
    {
        StartSpawnEnemy();
    }

    private void StartSpawnEnemy()
    {
        if (_enemyQueue.Count >= _enemyList.Count)
        {
            ReleaseEnemy();
        }
        else
        {
            SpawnEnemy();
        }
    }

    private void InitEnemy()
    {
        int enemyCount = (_enemyCount > _returnPoint.Count) ? _returnPoint.Count : _enemyCount;
        for (int i = 0; i < enemyCount; i++)
        {
            EnemyBehaviour enemy = Instantiate<EnemyBehaviour>(_enemyPrefabs, _returnPoint[i].position, Quaternion.identity);
            enemy.name = "Enemy" + (i + 1);
            foreach (Transform waypoint in _waypoints)
            {
                enemy.AddWaypoint(waypoint);
            }
            enemy.InitEnemy(_spawnPoint, _returnPoint[i], ReturnEnemy);
            _enemyList.Add(enemy);
            _enemyQueue.Enqueue(enemy);
        }
    }

    private void SpawnEnemy()
    {
        if (_enemyQueue.Count > 0 && _isSpawning == false)
        {
            _enemyRelease = StartCoroutine(PrepareEnemyRelease());
        }
    }

    private IEnumerator PrepareEnemyRelease()
    {
        _isSpawning = true;
        yield return new WaitForSeconds(_releaseInterval);
        if (_enemyQueue.Count > 0)
        {
            ReleaseEnemy();
        }
    }

    private void ReleaseEnemy()
    {
        EnemyBehaviour enemy = _enemyQueue.Dequeue();
        enemy.transform.position = _spawnPoint.position;
        enemy.NavMeshAgent.enabled = true;
        enemy.SwitchState(enemy.NeutralState);
        _isSpawning = false;
        SpawnEnemy();
    }
}
