using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Stats : MonoBehaviour
{
    [Space] public int Level = 1;
    public int NextLevel = 20;
    public int XP = 0;
    public int Money = 0;

    [Space] public int Points = 0;

    [Space] public int STR = 10;
    public int AGI = 25;
    public int CON = 10;
    public int Armor = 100;

    [Space] public int _maxHealth;
    public int _currentHealth;
    public int _minDamage;
    public int _maxDamage;
    private Animator _animator;
    private bool _isDeath = false;
    private bool _deathProcess = false;
    
    void Start()
    {
        CalculateStats();
        
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_deathProcess)
        {
            transform.Translate(Vector3.up * -Time.deltaTime * 0.5f);
        }
    }
    
    #region Stats
    
    public int GetHealth()
    {
        return _currentHealth;
    }

    public void Loot(int xp, int money)
    {
        XP += xp * Level;
        Money += money * Level;

        if (XP >= NextLevel)
        {
            LevelUp();
        }
    }

    public void SetLevel(int level)
    {
        Level = level;
        
        STR += STR / 10 * Level - 1;
        AGI += AGI / 15 * Level - 1;
        CON += CON / 7 * Level - 1;
        
        CalculateStats();
    }
    
    void LevelUp()
    {
        Level++;
        Points ++;

        NextLevel = 20 * Level + NextLevel;
        XP = 0;
        CalculateStats();
    }
    
    public void UpSTR()
    {
        STR++;
        CalculateStats();
    }

    public void UpAGI()
    {
        AGI++;
    }

    public void UpCON()
    {
        CON++;
        CalculateStats();
    }

    public void UpdateArmor(int oldArmor, int newArmor)
    {
        Armor += newArmor - oldArmor;
    }
    
    void CalculateStats()
    {
        _maxHealth = CON * 5;
        _currentHealth = _maxHealth;
        
        _minDamage = STR / 2;
        _maxDamage = _minDamage + 4;
    }
    
    #endregion Stats

    public int DealDamage()
    {
        return Random.Range(_minDamage, _maxDamage);
    }

    public float CurrentHealthUI()
    {
        return (float)_currentHealth / _maxHealth;
    }

    public float CurrentXPUI()
    {
        return (float)XP / NextLevel;
    }

    public void LootLife()
    {
        _currentHealth += Mathf.FloorToInt((float)_maxHealth * 0.3f);

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
    
    public void TakeDamage(int value)
    {
        _currentHealth -= Mathf.FloorToInt(value * (1.0f - Armor / 200.0f));

        if (_currentHealth <= 0)
        {
            if (CompareTag("Enemy"))
            {
                GetComponent<DropItems>().DropLife();
            }
            
            _isDeath = true;
            
            _animator.SetBool("Death", true);
            StartCoroutine(StartDeath());
        }
    }

    public bool IsDeath()
    {
        return _isDeath;
    }

    IEnumerator StartDeath()
    {
        yield return new WaitForSeconds(2.0f);

        GetComponent<NavMeshAgent>().enabled = false;
        _deathProcess = true;

        yield return new WaitForSeconds(2.0f);
        
        Destroy(gameObject);
    }
}
