using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobeController : MonoBehaviour
{
    public GameObject CheckPoint;
    
    public float AttackDistance = 1.5f;
    public float RangeVision = 10.0f;

    private Stats _stats;
    private MobeMove _mobeMove;
    private MobeAttack _mobeAttack;
    
    private GameObject _player;
    private Stats _statsPlayer;
    private bool _isVisible = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _statsPlayer = _player.GetComponent<Stats>();
        
        _stats = GetComponent<Stats>();
        _mobeMove = GetComponent<MobeMove>();
        _mobeAttack = GetComponent<MobeAttack>();
    }
    
    void Update()
    {
        if (_stats.IsDeath())
        {
            _mobeAttack.OffAttack();
        }
        
        CheckMove();
        CheckDistance();
    }

    void CheckDistance()
    {
        if (!_statsPlayer.IsDeath())
        {
            if (_isVisible
                && !_stats.IsDeath()
                && !_mobeAttack.IsAttack()
                && Vector3.Distance(transform.position, _player.transform.position) <= AttackDistance)
            {
                StartAttack();
            }
        }
        else if (_isVisible)
        {
            _isVisible = false;
            _mobeMove.Move(CheckPoint);
        }
    }

    void CheckMove()
    {
        if (!_stats.IsDeath()
            && !_statsPlayer.IsDeath())
        {
            if (!_isVisible)
            {
                if (Vector3.Distance(transform.position, _player.transform.position) < RangeVision)
                {
                    _isVisible = true;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, _player.transform.position) > AttackDistance)
                {
                    _mobeAttack.OffAttack();
                    _mobeMove.Move(_player);
                }
            }
        }
    }

    void StartAttack()
    {
        _mobeMove.OffMove();

        _mobeAttack.StartAttack(_player);
    }
}
