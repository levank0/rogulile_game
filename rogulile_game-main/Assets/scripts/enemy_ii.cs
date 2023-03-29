using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_ii : MonoBehaviour
{
    [SerializeField] private List<Transform> _pathPoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _chaseRadius;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Transform _target;

    private int _currentPathIndex;
    private bool _isChasing;

    private void Awake()
    {
        _currentPathIndex = 0;
    }

    private void Start()
    {
        transform.position = _pathPoints[_currentPathIndex].position;
    }

    private void Update()
    {
        if (_isChasing)
        {
            MoveTowardsTarget();
        }
        else
        {
            MoveAlongPath();
        }

        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _chaseRadius, _playerLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                _isChasing = true;
                _target = collider.transform;
            }
        }
    }

    private void MoveTowardsTarget()
    {
        if (_target)
        {
            Vector2 direction = (_target.position - transform.position).normalized;
            transform.position += (Vector3)direction * _speed * Time.deltaTime;
        }
        else
        {
            _isChasing = false;
        }
    }

    private void MoveAlongPath()
    {
        Vector2 targetPos = _pathPoints[_currentPathIndex].position;
        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;
        transform.position += (Vector3)direction * _speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            _currentPathIndex++;

            if (_currentPathIndex >= _pathPoints.Count)
            {
                _currentPathIndex = 0;
            }
        }
    }
}
