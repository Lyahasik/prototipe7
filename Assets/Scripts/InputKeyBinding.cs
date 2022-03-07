using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputKeyBinding : MonoBehaviour
{
    [SerializeField] private bool toggle;
    [SerializeField] private KeyCode key;

    public UnityEvent callback = new UnityEvent();

    [Serializable]
    public class ToggleEvent : UnityEvent<bool> {}
    public ToggleEvent toggleCallback = new ToggleEvent();
    
    [Serializable]
    public class FloatEvent : UnityEvent<float> {}
    public FloatEvent intCallback = new FloatEvent();

    private bool _state;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(key)) return;
        
        callback.Invoke();
        if (toggle)
        {
            _state = !_state;
            toggleCallback.Invoke(_state);
            intCallback.Invoke(_state ? 1 : 0);
        }
    }
}
