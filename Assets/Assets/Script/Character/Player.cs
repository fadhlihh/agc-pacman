using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpGone;

    [SerializeField]
    private EnemySpawner _enemySpawner;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Transform _spawnPosition;

    private CharacterController _characterController;
    private Animator _animator;
    private Coroutine _powerUpCoroutine;
    private bool _isPowerUpActive;

    public void ConsumePowerUp()
    {
        if (_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);
        }
        _powerUpCoroutine = StartCoroutine(ActivatePowerUp());
    }

    public void Dead()
    {
        _characterController.enabled = false;
        _gameManager?.SubtractHealth();
        _enemySpawner?.ReturnAllEnemy();
        Respawn();
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 axisDirection = new Vector3(horizontalAxis, 0, verticalAxis);

        if (axisDirection.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(axisDirection.x, axisDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }

        _animator?.SetFloat("Speed", axisDirection.magnitude);
        _characterController?.Move(axisDirection * _speed * Time.deltaTime);
    }

    private IEnumerator ActivatePowerUp()
    {
        _isPowerUpActive = true;
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(5);
        _isPowerUpActive = false;
        if (OnPowerUpGone != null)
        {
            OnPowerUpGone();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && _isPowerUpActive)
        {
            _scoreManager?.AddScore(100);
            other.gameObject.GetComponent<EnemyBehaviour>().ReturnToBase();
        }
    }

    private void Respawn()
    {
        transform.position = _spawnPosition.position;
        _characterController.enabled = true;
    }
}
