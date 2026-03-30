using System;
using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public Pathfinding pathfinder;
    public Transform target;
    public float speed = 5f;
    private Vector3 _currentTargetPosition;

    public Rigidbody2D rb;

    List<Node> _path;
    int _pathIndex;

    void Start()
    {
        _path = pathfinder.FindPath(transform.position, target.position);
        _currentTargetPosition = target.position;
        _pathIndex = 0;
    }

    void Update()
    {

        if (Vector3.Distance(_currentTargetPosition, target.position) > 0.1f)
        {
            _path = pathfinder.FindPath(transform.position, target.position);
            _currentTargetPosition = target.position;
            _pathIndex = 0;
        }

        if (_path == null || _pathIndex >= _path.Count)
            return;
    }

    private void FixedUpdate()
    {
        if (_path == null || _pathIndex >= _path.Count)
            return;

        Vector2 targetPos = _path[_pathIndex].worldPosition;
        Vector2 direction = (targetPos - rb.position).normalized;
        Vector2 force = direction * (speed * Time.fixedDeltaTime);
        rb.AddForce(force);
        
        if (Vector3.Distance(rb.position, targetPos) < 0.5f)
            _pathIndex++;
    }
    
    void OnDrawGizmos()
    {
        if (_path == null) return;

        for (int i = 0; i < _path.Count; i++)
        {
            Gizmos.color = _path[i].walkable ? Color.white : Color.red; 
            Gizmos.DrawWireCube(_path[i].worldPosition, Vector3.one);
        }
    }
}