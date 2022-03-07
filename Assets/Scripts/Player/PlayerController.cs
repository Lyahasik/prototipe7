using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public MenuManager GameManager;
    public float AttackDistance = 3.0f;

    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;
    
    private GameObject _target;
    private Stats _statsTarget;
    private Stats _stats;
    public Stats PlayerStats => _stats;

    [SerializeField] private Rect noClickZone;
    private bool canClick = true;
    
    void Start()
    {
        Instance = this;
        _playerMove = GetComponent<PlayerMove>();
        _playerAttack = GetComponent<PlayerAttack>();
        _stats = GetComponent<Stats>();
    }
    
    void Update()
    {
        if (!_stats.IsDeath())
        {
            if (Input.GetMouseButtonDown(0) && !noClickZone.Contains(Input.mousePosition) && canClick)
            {
                RaycastHit hit;
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    CheckMove(hit);
                }
            }

            CheckDistance();
        }
    }

    void CheckDistance()
    {
        if (_target
            && !_statsTarget.IsDeath()
            && !_playerAttack.IsAttack()
            && Vector3.Distance(transform.position, _target.transform.position) <= AttackDistance)
        {
            StartAttack();
        }
    }

    void StartAttack()
    {
        _playerMove.OffMove();

        GameManager.SetStatsEnemy(_target);
        _playerAttack.StartAttack(_target);
    }

    void CheckMove(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            _target = hit.collider.gameObject;
            _statsTarget = _target.GetComponent<Stats>();
            
            if (Vector3.Distance(transform.position, hit.collider.gameObject.transform.position) > AttackDistance)
            {
                GameManager.OffStatsEnemy();
                _playerAttack.OffAttack();
                _playerMove.Move(hit.collider.gameObject);
            }
            else
            {
                StartAttack();
            }
        }
        else
        {
            _target = null;
            GameManager.OffStatsEnemy();
            _playerAttack.OffAttack();
            _playerMove.Move(hit.point);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Life"))
        {
            _stats.LootLife();
            Destroy(other.gameObject);
        }
    }
    
    public async Task<Vector3?> WaitForTarget() // TODO Relocate to player controller
    {
        canClick = false;
        await Task.Yield();
        while (!Input.GetMouseButtonDown(0))
            await Task.Yield();
        await Task.Yield();
        canClick = true;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit)) return null;

        return hit.point;
    }
}
