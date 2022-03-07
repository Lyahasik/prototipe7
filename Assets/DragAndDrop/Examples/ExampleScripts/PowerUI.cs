using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DragAndDrop;

public class PowerUI : Draggable
{
    public bool buy;
    public Image icon;
    public Text label;

    private Button _button;
    
    public override void UpdateObject()
    {
        Power p = obj as Power;

        _button = GetComponent<Button>();
        
        _button.onClick.RemoveAllListeners();
        
        // if we have a power
        if (p != null)
        {
            // set the icon
            if (icon)
            {
                icon.sprite = p.icon;
                icon.color = p.color;
            }
            // set the label
            if (label)
                label.text = p.name;
            Debug.Log(p + " listen", p);
            if (buy)
            {
                DeactivateSlot();
                _button.onClick.AddListener(() =>
                {
                    PlayerController.Instance.PlayerStats.Points--;
                    PowerSetUI psu = (PowerSetUI)this.slot.container;
                    psu.SetCanDrag(true);
                });
            }
            else
                _button.onClick.AddListener(() => p.Cast());
        }
        
        // turn off if there is no Power
        gameObject.SetActive(p != null);
        
    }

    private async void DeactivateSlot()
    {
        await Task.Yield();
        await Task.Yield();
        PowerSetUI psu = (PowerSetUI)this.slot.container;
        psu.SetCanDrag(false);
    }
    
    void Update()
    {
        if (!buy || !_button) return;
        
        bool noPoints = PlayerController.Instance.PlayerStats.Points == 0;
        
        if (noPoints != _button.interactable) return;

        _button.interactable = !noPoints;
    }
}
