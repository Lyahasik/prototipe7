using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHook : MonoBehaviour
{
    public GameObject Player;

    private Vector3 _hookPosition;
    
    void Start()
    {
        _hookPosition = transform.position - Player.transform.position;
    }

    void Update()
    {
        if (Player)
        {
            transform.position = Player.transform.position + _hookPosition;
        }
    }
}
