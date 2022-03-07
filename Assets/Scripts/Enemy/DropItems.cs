using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    public GameObject PrefabLife;
    public int LifeRandom = 70;

    [Space] public GameObject[] PrefabWeapons;
    public int WeaponRandom = 10;

    public void DropLife()
    {
        if (Random.Range(0, 100) < LifeRandom)
        {
            Instantiate(PrefabLife, transform.position + Vector3.up, transform.rotation);
        }
    }

    public void DropWeapon()
    {
        if (Random.Range(0, 100) < WeaponRandom)
        {
            Instantiate(PrefabWeapons[Random.Range(0, PrefabWeapons.Length)], transform.position + ShiftDrop(), transform.rotation);
        }
    }

    Vector3 ShiftDrop()
    {
        return new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
    }
}
