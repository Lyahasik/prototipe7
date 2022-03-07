using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Stats StatsPlayer;
    public PlayerAttack AttackPlayer;
    public Text TextPlayerLevel;
    public Image ImagePlayerHealth;
    public Image ImagePlayerXP;

    [Space] public GameObject MenuStatsEnemy;
    public Text TextEnemyLevel;
    public Image ImageEnemyHealth;
    
    [Space] public GameObject MenuFullStats;
    public Text TextSTR;
    public Text TextAGI;
    public Text TextCON;
    public Text TextMinDmg;
    public Text TextMaxDmg;
    public Text TextMaxHp;
    public Text TextNextLvl;

    private Stats _statsEnemy;

    void Update()
    {
        PlayerUpdate();
        CheckEnemy();
        EnemyUpdate();
        ViewFullStats();
    }

    void ViewFullStats()
    {
        if (Input.GetKeyDown("tab"))
        {
            MenuFullStats.SetActive(true);
        }
        
        if (Input.GetKeyUp("tab"))
        {
            MenuFullStats.SetActive(false);
        }
    }

    void CheckEnemy()
    {
        if (!AttackPlayer.IsAttack())
        {
            RaycastHit hit;
            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)
                && hit.collider.CompareTag("Enemy"))
            {
                SetStatsEnemy(hit.collider.gameObject);
            }
            else
            {
                OffStatsEnemy();
            }
        }
    }

    void PlayerUpdate()
    {
        TextPlayerLevel.text = StatsPlayer.Level.ToString();
        ImagePlayerHealth.fillAmount = StatsPlayer.CurrentHealthUI();
        ImagePlayerXP.fillAmount = StatsPlayer.CurrentXPUI();
        
        TextSTR.text = StatsPlayer.STR.ToString();
        TextAGI.text = StatsPlayer.AGI.ToString();
        TextCON.text = StatsPlayer.CON.ToString();
        TextMinDmg.text = StatsPlayer._minDamage.ToString();
        TextMaxDmg.text = StatsPlayer._maxDamage.ToString();
        TextMaxHp.text = StatsPlayer._maxHealth.ToString();
        TextNextLvl.text = (StatsPlayer.NextLevel - StatsPlayer.XP).ToString();
    }

    void EnemyUpdate()
    {
        if (_statsEnemy)
        {
            TextEnemyLevel.text = _statsEnemy.Level.ToString();
            ImageEnemyHealth.fillAmount = _statsEnemy.CurrentHealthUI();
        }
    }

    public void SetStatsEnemy(GameObject enemy)
    {
        _statsEnemy = enemy.GetComponent<Stats>();
        MenuStatsEnemy.SetActive(true);
    }

    public void OffStatsEnemy()
    {
        _statsEnemy = null;
        MenuStatsEnemy.SetActive(false);
    }
}
