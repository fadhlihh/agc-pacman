using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpGone;

    [SerializeField]
    private InputController _inputController;
    [SerializeField]
    private EnemySpawner _enemySpawner;
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
        _enemySpawner.ReturnAllEnemy();
        Respawn();
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _inputController.OnMove += Move;
    }

    private void Move(Vector3 direction)
    {
        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }

        _animator?.SetFloat("Speed", direction.magnitude);
        _characterController?.Move(direction * _speed * Time.deltaTime);
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
            other.gameObject.GetComponent<EnemyBehaviour>().ReturnToBase();
        }
    }

    private void Respawn()
    {
        transform.position = _spawnPosition.position;
        _characterController.enabled = true;
    }
}
