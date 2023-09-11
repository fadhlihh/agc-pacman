using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Vector3 _offset;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position + _offset, 5);
    }
}
