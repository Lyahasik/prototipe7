using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Stats PlayerStats;
    public float DelaySpawn = 10.0f;
    public GameObject[] PrefabMobs;

    private GameObject _mobe;
    private float _timeSpawn;

    private bool _isDead = false;

    private void Start()
    {
        _timeSpawn = Time.time + 1.0f;

        NewSpawn();
    }

    private void Update()
    {
         NewSpawn();
    }

    void NewSpawn()
    {
        if (!_mobe
            && !_isDead)
        {
            _timeSpawn = Time.time + DelaySpawn;
            _isDead = true;
        }
        
        if (!_mobe
            && _timeSpawn < Time.time)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        _mobe = Instantiate(PrefabMobs[Random.Range(0, 2)], transform.position, transform.rotation);
        _mobe.GetComponent<MobeController>().CheckPoint = gameObject;
        _mobe.GetComponent<Stats>().SetLevel(PlayerStats.Level);
        _isDead = false;
    }
}
