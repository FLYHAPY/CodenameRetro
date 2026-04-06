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

    private bool _isAttacking;
    
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float attackForce;

    List<Node> _path;
    int _pathIndex;

    [SerializeField] private float radius;
    [SerializeField] private float damage;
    [SerializeField] private float knockbackForce;

    void Start()
    {
        _path = pathfinder.FindPath(transform.position, target.transform.position);
        _currentTargetPosition = target.transform.position;
        _pathIndex = 0;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= radius)
        {
            _isAttacking =  true;
            anim.SetBool("isAttacking", true);
        }

        if (Vector3.Distance(_currentTargetPosition, target.transform.position) > 0.1f && !_isAttacking)
        {

            _path = pathfinder.FindPath(transform.position, target.transform.position);
            _currentTargetPosition = target.position;
            _pathIndex = 0;
        }
    }

    private void FixedUpdate()
    {
        if (_path == null || _pathIndex >= _path.Count || _isAttacking )
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
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        
        if (_path == null) return;

        for (int i = 0; i < _path.Count; i++)
        {
            Gizmos.color = _path[i].walkable ? Color.white : Color.red; 
            Gizmos.DrawWireCube(_path[i].worldPosition, Vector3.one);
        }
    }

    public void Attack()
    {
        Vector2 direction = ((Vector2)target.transform.position - rb.position).normalized;
        Vector2 force = direction * (attackForce * Time.fixedDeltaTime);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void ResetAttack()
    {
        anim.SetBool("isAttacking", false);
        _isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().ReduceHealth(damage);
            /*Vector2 direction = (rb.position - (Vector2)other.transform.position).normalized;
            other.GetComponent<Rigidbody2D>().AddForce(-direction  * knockbackForce,  ForceMode2D.Impulse);*/
        }
    }
}