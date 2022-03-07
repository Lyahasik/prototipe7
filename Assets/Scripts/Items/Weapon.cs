using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    public float Speed = 0.7f;
    public float Damage = 12.0f;

    private void Start()
    {
        Stats playerStats = GameObject.Find("Player").GetComponent<Stats>();
        
        Speed += Random.Range(-0.2f, 0.2f);
        Damage += Random.Range(-Damage / 2, Damage / 2);
        Damage += playerStats.Level * 2;
    }
}
