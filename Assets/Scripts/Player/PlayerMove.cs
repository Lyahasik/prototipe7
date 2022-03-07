using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Stats _stats;

    private GameObject _target;
    private Vector3 _point;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _stats = GetComponent<Stats>();
    }

    private void Update()
    {
        if (!_stats.IsDeath()
            && _target
            && Vector3.Distance(_target.transform.position, transform.position) < 0.2f)
        {
            OffMove();
        }
        
        if (!_stats.IsDeath()
            && !_target
            && Vector3.Distance(_point, transform.position) < 0.2f)
        {
            OffMove();
        }
    }

    public void Move(GameObject target)
    {
        _target = target;

        if (Vector3.Distance(_target.transform.position, transform.position) > 0.2f)
        {
            OnMove();
            _navMeshAgent.destination = _target.transform.position;
        }
    }

    public void Move(Vector3 point)
    {
        _point = point;

        if (Vector3.Distance(_point, transform.position) > 0.2f)
        {
            OnMove();
            _navMeshAgent.destination = _point;
        }
    }

    void OnMove()
    {
        _animator.SetBool("Move", true);
        _navMeshAgent.isStopped = false;
    }

    public void OffMove()
    {
        _target = null;
        _animator.SetBool("Move", false);
        _navMeshAgent.isStopped = true;
    }
}
