using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobeMove : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private GameObject _target;
    
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (_target
            && Vector3.Distance(_target.transform.position, transform.position) < 0.2f)
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

    void OnMove()
    {
        _animator.SetBool("Move", true);
        _navMeshAgent.isStopped = false;
    }

    public void OffMove()
    {
        _animator.SetBool("Move", false);
        _navMeshAgent.isStopped = true;
    }
}
