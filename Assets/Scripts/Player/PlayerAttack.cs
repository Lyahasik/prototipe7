using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    public float SpeedAttack = 0.7f;
    
    private Animator _animator;
    private Stats _stats;
    
    private GameObject _target;
    private Stats _statsTarget;
    private bool _isAttack = false;

    private float _timeAttack;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _stats = GetComponent<Stats>();
    }

    private void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (_isAttack
            && _timeAttack < Time.time)
        {
            _timeAttack = Time.time + SpeedAttack;

            if (Random.Range(0, 100) < 75 + _stats.AGI - _statsTarget.AGI)
            {
                _statsTarget.TakeDamage(_stats.DealDamage());
            }

            if (_statsTarget.IsDeath())
            {
                _stats.Loot(_statsTarget.XP, _statsTarget.Money);
                OffAttack();
            }
        }
    }

    public void StartAttack(GameObject target)
    {
        if (!_target)
        {
            _target = target;
            _statsTarget = _target.GetComponent<Stats>();

            _isAttack = true;
            _timeAttack = Time.time + SpeedAttack;
            _animator.SetBool("Attack", true);

            transform.LookAt(target.transform.position, Vector3.up);
        }
    }

    public bool IsAttack()
    {
        return _isAttack;
    }

    public void OffAttack()
    {
        _target = null;
        _isAttack = false;
        _animator.SetBool("Attack", false);
    }
}
