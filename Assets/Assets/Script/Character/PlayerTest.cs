using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private CharacterController _characterController;
    private Animator _animator;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
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

    private void OnCollisionEnter(Collision other)
    {
        // if (other.gameObject.CompareTag("Enemy") && _isPowerUpActive)
        // {
        //     _scoreManager.AddScore(100);
        //     other.gameObject.GetComponent<EnemyBehaviour>().ReturnToBase();
        // }
    }
}
